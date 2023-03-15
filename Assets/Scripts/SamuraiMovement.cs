using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiMovement : MonoBehaviour
{
    public float speed = 3f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float attackTime = 2f;
    public float aggroRange = 12f;
    public float leftStoppingPoint = -5f;
    public float rightStoppingPoint = 5f;
    public GameObject playerObject;
    public AudioClip swordClip;

    private float _timeToFire;
    private Vector2 direction = Vector2.left;
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    private bool aggro = false, playerInReach = false;
    private Rigidbody2D _rigidbody;
    private Collider2D[] colliders;
    private AudioSource samuraiAudioSource;
    void Start()
    {
        samuraiAudioSource = gameObject.AddComponent<AudioSource>();
        colliders = GetComponentsInChildren<Collider2D>();
        _timeToFire = 0;
        leftPoint = new Vector2(transform.position.x + leftStoppingPoint, 0f);
        rightPoint = new Vector2(transform.position.x + rightStoppingPoint, 0f);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        float distToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);

        if (distToPlayer < aggroRange)
            aggro = true;
        else
            aggro = false;

        if (aggro)
        {
            Vector2 playerDirection = (playerObject.transform.position - transform.position).normalized;
            _rigidbody.velocity = playerDirection * speed;
        }

        else
        {

            if (transform.position.x >= rightPoint.x)
            {
                spriteRenderer.flipX = false;
                direction = Vector2.left;
            }
            if (transform.position.x <= leftPoint.x)
            {
                spriteRenderer.flipX = true;
                direction = Vector2.right;
            }
        }

        _rigidbody.velocity = direction * speed;

        _timeToFire -= Time.deltaTime;

        animator.SetFloat("speed", _rigidbody.velocity.magnitude);

        if (playerInReach == true && _timeToFire <= 0f)
        {
            _timeToFire = attackTime;
            animator.SetTrigger("attack");
            samuraiAudioSource.PlayOneShot(swordClip);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            playerInReach = true;
        }

        /*
        if (other.gameObject.tag == "Fireball")
        {
            Destroy(gameObject);
        }
        */
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            playerInReach = false;
        }

    }
}
