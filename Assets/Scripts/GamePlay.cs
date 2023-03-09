using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
