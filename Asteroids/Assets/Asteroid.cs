using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : SpaceObject
{

    private Vector2 direction = Vector2.zero;

    public GameObject asteroid_sprite;
    public Sprite[] array_of_sprites;
    private float spin_speed = 0f;

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
}
