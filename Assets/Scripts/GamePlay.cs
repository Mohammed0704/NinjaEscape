using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public GameOverScreen gameoverscreen;
    public PlayerHealth PlayerHealth;

    void Update()
    {
        if (PlayerHealth.health <= 0){
            StartCoroutine(WaitAndCallGameOver());
        }
    }

    public void PlayGame(){
        SceneManager.LoadScene("LevelOne");
    }
    
    public void GameOver(){
        Time.timeScale = 0;
        gameoverscreen.Setup();
    }

    private IEnumerator WaitAndCallGameOver(){
        yield return new WaitForSeconds(1.0f);
        GameOver();
    }

    public void Quit(){
        Application.Quit();
    }

    public void Levels(){
        SceneManager.LoadScene("LevelSelector");
    }

    public void LevelOne(){
        SceneManager.LoadScene("LevelOne");
    }

    public void LevelTwo(){
        SceneManager.LoadScene("LevelTwo");
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu(int sceneID){
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneID);
        PlayerHealth.health = 5;
    }

    public void RestartLevel() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        PlayerHealth.health = 5;
    }

}
