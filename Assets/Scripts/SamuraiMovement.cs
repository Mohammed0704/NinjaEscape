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
    public AudioClip swordClip;

    private float _timeToFire;
    private GameObject playerObject;
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
        playerObject = GameObject.FindGameObjectWithTag("Player");
        _timeToFire = 0;
        leftPoint = new Vector2(transform.position.x + leftStoppingPoint, 0f);
        rightPoint = new Vector2(transform.position.x + rightStoppingPoint, 0f);
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);

        if (distToPlayer < aggroRange)
            aggro = true;
        else
            aggro = false;

        if (aggro)
        {
            direction = (playerObject.transform.position - transform.position).normalized;
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
    }

    void FixedUpdate()
    {
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
