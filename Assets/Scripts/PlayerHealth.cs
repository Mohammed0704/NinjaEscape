using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static int health = 5;
   
    //public GamePlay gameplay;
    public AudioClip playerDeathClip;
    private AudioSource playerAudioSource;

    public Slider healthBar;

    // Update is called once per frame
    
    /*
    void Update()
    {
        if (health == 0)
        {   
            
            playerAudioSource.PlayOneShot(playerDeathClip);
            StartCoroutine(WaitAndCallGameOver());
            /*
            gameOver.SetActive(true);
            try
            {
                gameOver.GetComponent<GameOverScreen>().GameOver();
            }
            catch
            { }
            Time.timeScale = 0f;
            
        }
    }
    */

    void Start()
    {
        playerAudioSource = gameObject.AddComponent<AudioSource>();
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        healthBar.maxValue = health;

    }

    public void deductHealth()
    {
        health--;
        healthBar.value = health;
        StartCoroutine(FlashRed());
    }

    IEnumerator FlashRed()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color og_color = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.5f);
        spriteRenderer.color = og_color;
    }

    public void incrementHealth()
    {
        health++;
        healthBar.value = health;
    }
}
