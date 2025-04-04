using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    private Vector2 input_vec = Vector2.zero;
    private float rotate_speed = 200f;
    private Rigidbody2D rb;
    private float max_speed = 5.5f;
    private float acceleration = 13;
    private Vector2 velocity = Vector2.zero;

    //screen wrapping
    public Camera cam;
    private Vector2 cam_bottom_left = Vector2.zero;
    private Vector2 cam_top_right = Vector2.zero;
    private float wrap_radius = 0.16f;

    //Ships visuals
    public GameObject ship_sprite;
    private float jitter_amount = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();

        cam_top_right = new Vector2(cam.scaledPixelWidth, cam.scaledPixelHeight);
        cam_bottom_left = cam.ScreenToWorldPoint(cam_bottom_left);
        cam_top_right = cam.ScreenToWorldPoint(cam_top_right);
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

            //jitter
            Vector2 jitter_pos = new Vector2(Random.Range(-jitter_amount, jitter_amount), Random.Range(-jitter_amount, jitter_amount));
            ship_sprite.transform.localPosition = jitter_pos;
        }

        rb.MovePosition(rb.position + (velocity *Time.fixedDeltaTime));

        #region Screen Wrap

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
