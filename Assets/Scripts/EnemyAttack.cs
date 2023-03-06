using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttack : MonoBehaviour
{
    public float attackTime;
    public Animator animator;
    public GameObject player, sword;
    private SwordColliderDetection detector;

    private void Start()
    {
        detector = sword.GetComponent<SwordColliderDetection>();
    }
    public void SamuraiAttack()
    {
        if (detector.playerCollision == true)
        {
            Destroy(player);
        }
    }
}
