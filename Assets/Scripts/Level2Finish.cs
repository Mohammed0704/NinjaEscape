using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Finish : MonoBehaviour
{
    public GamePlay gamePlay;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            gamePlay.GameOver();
        }
    }
}
