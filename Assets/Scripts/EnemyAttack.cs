using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAttack : MonoBehaviour
{
    public GameObject player, sword;
    private SwordColliderDetection detector;
    private Slider playerHealthBar;

    private void Start()
    {
        playerHealthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        detector = sword.GetComponent<SwordColliderDetection>();
    }
    public void SamuraiAttack()
    {
        if (detector.playerCollision == true)
        {
            player.GetComponent<PlayerHealth>().deductHealth();
        }
    }
}
