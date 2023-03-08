using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiMovement : MonoBehaviour
{
    public float speed = 3f;
    public Animator animator;
    public float attackTime;
    private float _timeToFire;

    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _timeToFire -= Time.deltaTime;
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Vector2 playerDirection = (playerObject.transform.position - transform.position).normalized;
            _rigidbody.velocity = playerDirection * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player") && _timeToFire <= 0f)
        {
            _timeToFire = attackTime;
            animator.SetTrigger("attack");
        }

        if (other.gameObject.tag == "Fireball")
        {
            Destroy(gameObject);
        }
    }
}
