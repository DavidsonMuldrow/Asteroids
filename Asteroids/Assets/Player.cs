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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D> ();

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
        }

        rb.MovePosition(rb.position + (velocity *Time.fixedDeltaTime));

    }

    public void CaptureMoveInput(InputAction.CallbackContext context)
    {
        input_vec = context.ReadValue<Vector2>();
    }
}
