using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAttack : MonoBehaviour
{
    public GameObject sword;
    private SwordColliderDetection detector;

    private void Start()
    {
        detector = sword.GetComponent<SwordColliderDetection>();
    }
    public void SamuraiAttack()
    {
        if (detector.playerCollision == true)
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerHealth>().deductHealth();
        }
    }
}
