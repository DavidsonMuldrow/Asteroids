using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small_asteroid : Asteroid
{
    void Start()
    {
        FindCameraBounds();

        rb = GetComponent<Rigidbody2D>();
        wrap_radius = 0.32f;
        max_speed = Random.Range(1.4f, 2.1f);
        float start_angle = Random.Range(0, 360);
        start_angle *= Mathf.Deg2Rad;
        direction.x = Mathf.Cos(start_angle);
        direction.y = Mathf.Sin(start_angle);
        velocity = direction * max_speed;

        asteroid_sprite.GetComponent<SpriteRenderer>().sprite = array_of_sprites[Random.Range(0, array_of_sprites.Length)];

        spin_speed = Random.Range(-40f, 40f);

        health = 1;
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

                Events.Asteroid_Was_Destroyed?.Invoke();
            }
        }
    }
}
