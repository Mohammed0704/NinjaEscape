using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    public Image black;
    public Animator anim;
    public int index;
    [SerializeField] GameObject nextLevel;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            nextLevel.SetActive(true);
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading(){
        anim.SetBool("Fade", true);
        //yield return new WaitUntil(()=>black.color.a==1);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(index);
    }
}
