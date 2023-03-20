using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject gameOver;

    public void GameOver() {
        gameOver.SetActive(true);
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            GameOver();
            Time.timeScale = 0f;
        }
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelOne"); 
    }
}
