using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : SpaceObject
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        FindCameraBounds();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (velocity * Time.fixedDeltaTime));

        if (rb.position.x - wrap_radius > cam_top_right.x)
        {
            Destroy(gameObject);
        }

        if (rb.position.x + wrap_radius < cam_bottom_left.x)
        {
            Destroy(gameObject);
        }

        if (rb.position.y - wrap_radius > cam_top_right.y)
        {
            Destroy(gameObject);
        }

        if (rb.position.y + wrap_radius < cam_bottom_left.y)
        {
            Destroy(gameObject);
        }
    }

    public void SetUp(Vector2 direction)
    {
        max_speed = 8;
        velocity = max_speed * direction;
    }
}
