using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PlayGame(){
        SceneManager.LoadScene("LevelOne");
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
        Time.timeScale = 1f;
    }

    public void MainMenu(int sceneID){
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }

    public void RestartLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

}
