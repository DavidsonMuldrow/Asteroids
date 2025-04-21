using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;
    private Vector2 direction = Vector2.zero;

    public GameObject debris_sprite;
    public Sprite[] array_of_sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(velocity.x, velocity.y, 0) * Time.deltaTime;

        debris_sprite.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
    }

    public void SetUp(int i)
    {
        //pick a starting angle
        float start_angle = 120 * i;
        //convert the angle into a vector
        start_angle *= Mathf.Deg2Rad;
        direction.x = Mathf.Cos(start_angle);
        direction.y = Mathf.Sin(start_angle);
        //calculate velocity
        velocity = direction * 3.5f;
        //set the sprite 
        debris_sprite.GetComponent<SpriteRenderer>().sprite = array_of_sprites[i];
    }
}
