using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SpaceObject
{

    private Vector2 input_vec = Vector2.zero;
    private float rotate_speed = 200f;
    private float acceleration = 13;

    //screen wrapping

    //Ships visuals
    public GameObject ship_sprite;
    private float jitter_amount = 0.04f;

    public GameObject fire_sprite;

    //Shooting
    public GameObject bullet;
    private float rate_of_fire = 0.1f;
    private float shoot_timer = 0;

    public GameObject debris;

    private float spam_timer = 0;

    public AudioClip fire;

    // Start is called before the first frame update
    void Start()
    {
        fire_sprite.SetActive(false);

        rb = GetComponent<Rigidbody2D> ();
        max_speed = 5.5f;
        wrap_radius = 0.16f;

        FindCameraBounds();
    }

    private void Update()
    {
        if (shoot_timer > 0)
        {
            shoot_timer -= Time.deltaTime;
        }
        
        if (spam_timer <= 0)
        {
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (input_vec.x != 0)
        {
            rb.MoveRotation(rb.rotation + (-Mathf.Sign(input_vec.x) * rotate_speed * Time.fixedDeltaTime));
        }

        if (input_vec.y > 0)
        {
            velocity += new Vector2(transform.up.x, transform.up.y) * acceleration * Time.fixedDeltaTime;

            velocity = Vector2.ClampMagnitude(velocity, max_speed);

            //jitter and fire sprite
            Vector2 jitter_pos = new Vector2(Random.Range(-jitter_amount, jitter_amount), Random.Range(-jitter_amount, jitter_amount));
            ship_sprite.transform.localPosition = jitter_pos;

            fire_sprite.SetActive(true);
            fire_sprite.transform.localPosition = new Vector2(Random.Range(-jitter_amount, jitter_amount) + 0.01f, Random.Range(-jitter_amount, jitter_amount) - 0.25f);
        } else
        {
            ship_sprite.transform.localPosition = Vector2.zero;

            fire_sprite.SetActive(false);
        }

        rb.MovePosition(rb.position + (velocity *Time.fixedDeltaTime));

        #region Screen Wrap
        ScreenWrap();
        #endregion
    }

    public void CaptureMoveInput(InputAction.CallbackContext context)
    {
        input_vec = context.ReadValue<Vector2>();
    }

    public void CaptureShootInput(InputAction.CallbackContext context)
    {
        if (shoot_timer <= 0)
        {
            shoot_timer = rate_of_fire;

            GameObject new_bullet = Instantiate(bullet);

            new_bullet.GetComponent<Bullet>().SetUp(transform.up);

            new_bullet.transform.position = transform.position;

            AudioSource.PlayClipAtPoint(fire, transform.position);
        }
    }

    public void CaptureSpamInput(InputAction.CallbackContext context)
    {
        StartCoroutine(WaitInLoop());
        spam_timer = 15f;
        IEnumerator WaitInLoop()
        {


            while (spam_timer > 0)
            {
                GameObject new_bullet = Instantiate(bullet);
                new_bullet.GetComponent<Bullet>().SetUp(transform.up);
                new_bullet.transform.position = transform.position;
                spam_timer--;
                AudioSource.PlayClipAtPoint(fire, transform.position);
                yield return new WaitForSeconds(.3f);
            }   
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wrap_radius);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i <= 2; i++)
        {
            GameObject new_debris = Instantiate(debris);
            new_debris.transform.position = transform.position;

            new_debris.GetComponent<Debris>().SetUp(i);

            Destroy(new_debris, 1.5f);
        }

        Destroy(gameObject);
    }
}
