using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected float max_speed = 0f;
    protected Vector2 velocity = Vector2.zero;

    protected Vector2 cam_bottom_left = Vector2.zero;
    protected Vector2 cam_top_right = Vector2.zero;
    protected float wrap_radius = 0.0f;

    protected void FindCameraBounds()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam_top_right = new Vector2(cam.scaledPixelWidth, cam.scaledPixelHeight);
        cam_bottom_left = cam.ScreenToWorldPoint(cam_bottom_left);
        cam_top_right = cam.ScreenToWorldPoint(cam_top_right);
    }

    protected void ScreenWrap()
    {
        if (rb.position.x - wrap_radius > cam_top_right.x)
        {
            rb.MovePosition(new Vector2(cam_bottom_left.x - wrap_radius + 0.01f, rb.position.y));
        }

        if (rb.position.x + wrap_radius < cam_bottom_left.x)
        {
            rb.MovePosition(new Vector2(cam_top_right.x + wrap_radius - 0.01f, rb.position.y));
        }

        if (rb.position.y - wrap_radius > cam_top_right.y)
        {
            rb.MovePosition(new Vector2(rb.position.x, cam_bottom_left.y - wrap_radius + 0.01f));
        }

        if (rb.position.y + wrap_radius < cam_bottom_left.y)
        {
            rb.MovePosition(new Vector2(rb.position.x, cam_top_right.y + wrap_radius - 0.01f));
        }
    }
}
