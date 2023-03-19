using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject gameOver;

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            gameOver.GetComponent<GameOverScreen>().GameOver();
        }
    }

    public void deductHealth()
    {
        health--;
    }

    public void incrementHealth()
    {
        health++;
    }
}
