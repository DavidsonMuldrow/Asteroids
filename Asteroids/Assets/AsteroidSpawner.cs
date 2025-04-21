using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject asteroid;


    void Start()
    {
        Events.Asteroid_Was_Destroyed += spawn_asteroid;
    }

    void Update()
    {
        
    }

    private void spawn_asteroid()
    {
        int num = Random.Range(2, 5);
        for (var i = num -1; i < num; i++)
        {
            GameObject new_small_asteroid = Instantiate(asteroid);

            new_small_asteroid.transform.position = Vector3.zero;
        }
    }
}
