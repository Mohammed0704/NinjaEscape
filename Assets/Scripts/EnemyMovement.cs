using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f;
    public float sightRange = 3f;
    public float wanderTime = 3f;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

void FixedUpdate()
{
    GameObject playerObject = GameObject.FindWithTag("Player");
    if (playerObject != null)
    {
        Vector2 playerDirection = (playerObject.transform.position - transform.position).normalized;
        _rigidbody.velocity = playerDirection * speed;
    }
}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Fireball")
        {
            Destroy(gameObject);
        }
    }
}

