using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject asteroid;
    public TMP_Text score_text;
    private float score = 0;
    public AudioClip Explosion;

    void Start()
    {
        Events.Asteroid_Was_Destroyed += CoroutineStarter;
        Events.Add_Score += addToScore;
        score_text.text = "Score: 0";
    }

    void Update()
    {

    }

    private void spawn_asteroid()
    {
        if (AsteroidInScene() == false)
        {
            int num = Random.Range(2, 5);
            for (var i = num - 1; i < num; i++)
            {
                GameObject new_small_asteroid = Instantiate(asteroid);
            }
        }
    }

    private bool AsteroidInScene()
    {
        if (GameObject.FindWithTag("Asteroid") != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator DelayAsteroidSpawn()
    {
        yield return new WaitForSeconds(2f);
        spawn_asteroid();
    }

    private void CoroutineStarter()
    {
        StopAllCoroutines();
        StartCoroutine(DelayAsteroidSpawn());
    }

    private void addToScore()
    {
        score++;
        score_text.text = "Score: " + score.ToString();
        AudioSource.PlayClipAtPoint(Explosion, transform.position);
    }

}
