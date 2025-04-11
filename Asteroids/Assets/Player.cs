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

    // Start is called before the first frame update
    void Start()
    {
        fire_sprite.SetActive(false);

        rb = GetComponent<Rigidbody2D> ();
        max_speed = 5.5f;
        wrap_radius = 0.16f;

        FindCameraBounds();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wrap_radius);

    }
}
