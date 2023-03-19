using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float speed = 3f;
    public float leftStoppingPoint = -5f;
    public float rightStoppingPoint = 5f;
    public AudioClip deathClip, playerDeathClip;
    public GameObject player;

    private AudioSource enemyAudioSource;
    private Vector2 direction = Vector2.left, lastDir;
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    private bool goingRight=true;
    private Rigidbody2D _rigidbody;


    void Start()
    {
        enemyAudioSource = gameObject.AddComponent<AudioSource>();
        leftPoint = new Vector2(transform.position.x + leftStoppingPoint, 0f);
        rightPoint = new Vector2(transform.position.x + rightStoppingPoint, 0f);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.x >= rightPoint.x)
        {
            direction = Vector2.left;
        }
        if (transform.position.x <= leftPoint.x)
        {
            direction = Vector2.right;
        }

        if (direction.x * lastDir.x < 0)
            Flip();

        lastDir = direction;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = direction * speed;

    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyAudioSource.PlayOneShot(playerDeathClip);
            player.GetComponent<PlayerController>().PlayerDies();
        }

        if (other.gameObject.tag == "Fireball")
        {
            EnemyDies();
        }
    }

    void EnemyDies()
    {
        enemyAudioSource.PlayOneShot(deathClip);
        spriteRenderer.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, deathClip.length);
    }
}

