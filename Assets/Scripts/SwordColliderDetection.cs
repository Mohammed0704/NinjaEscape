using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderDetection : MonoBehaviour
{
    public bool playerCollision = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollision = false;
        }
    }
}
