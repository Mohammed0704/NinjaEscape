using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 5;
    [SerializeField] GameObject gameOver;
    public Slider healthBar;

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            gameOver.GetComponent<GameOverScreen>().GameOver();
        }
    }

    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBar.maxValue = health;
        
    }

    public void deductHealth()
    {
        health--;
        healthBar.value = health;
    }

    public void incrementHealth()
    {
        health++;
        healthBar.value = health;
    }
}


/*
public class PlayerHealth : MonoBehaviour
{
    public static int health = 5;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBar.maxValue = health;
        
    }

    //Checks for collision with spikes
    private void OnCollisionEnter2D (Collision2D collision){
        if (collision.gameObject.CompareTag("Player")){
            health--;
            healthBar.value = health;
        }
    }

    //Check for collision with ghost

    //Check for collision with samurai
}
*/
