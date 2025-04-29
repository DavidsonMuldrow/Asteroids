using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class small_asteroid : Asteroid
{
    private int score = 0;
    public TMP_Text score_text;
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

        score_text.text = "Score: 0";
    }

    private void Update()
    {
        score_text.text = "Score: " + score.ToString();
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
                score++;
                Events.Asteroid_Was_Destroyed?.Invoke();
            }
        }
    }
}
