using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float sightRange = 3f;
    public float wanderTime = 3f;

    private Rigidbody2D rigidbody;
    
    //private LayerMask layerMask;

    void Start()
    {
        //layerMask = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 playerDirection = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        rigidbody.velocity = playerDirection * speed;

        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)transform.position + playerDirection, sightRange, layerMask);
        Debug.DrawRay(transform.position, (Vector2)transform.position + playerDirection, Color.blue, sightRange);

        if (hit && hit.collider.CompareTag("Player"))
        {
            rigidbody.velocity = playerDirection * speed;
        }
        else
        {
            rigidbody.velocity = Vector2.zero;
        }
        */
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Ow");
            Destroy(other.gameObject);
        }
    }
}
