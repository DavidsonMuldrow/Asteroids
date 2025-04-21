using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : SpaceObject
{

    protected Vector2 direction = Vector2.zero;

    public GameObject asteroid_sprite;
    public Sprite[] array_of_sprites;
    protected float spin_speed = 0f;

    protected int health = 3;

    [SerializeField] private GameObject small_asteroid;

    // Start is called before the first frame update
    void Start()
    {
        FindCameraBounds();

        rb = GetComponent<Rigidbody2D>();
        wrap_radius = 0.64f;
        max_speed = Random.Range(0.65f, 1.1f);
        float start_angle = Random.Range(0, 360);
        start_angle *= Mathf.Deg2Rad;
        direction.x = Mathf.Cos(start_angle);
        direction.y = Mathf.Sin(start_angle);
        velocity = direction * max_speed;

        asteroid_sprite.GetComponent<SpriteRenderer>().sprite = array_of_sprites[Random.Range(0, array_of_sprites.Length)];

        spin_speed = Random.Range(-20f, 20f);
    }

    void Update()
    {
        asteroid_sprite.transform.Rotate(Vector3.forward, spin_speed * Time.deltaTime);
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));

        #region ScreenWrap
        ScreenWrap();
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            health--;

            if (health <= 0)
            {
                Destroy(gameObject);

                spawn_small_asteroid();
            }
        }
    }

    private void spawn_small_asteroid()
    {
        int num = Random.Range(2, 5);
        for (var i = 0; i < num; i++)
        {
            GameObject new_small_asteroid = Instantiate(small_asteroid);

            new_small_asteroid.transform.position = transform.position;
        }
    }
}
