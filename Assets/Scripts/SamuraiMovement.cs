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

    private float _timeToFire, set_speed, og_x;
    private GameObject playerObject;
    private Vector2 direction = Vector2.left, lastDir;
    private Vector2 leftPoint;
    private Vector2 rightPoint;
    private bool aggro = false, playerInReach = false;
    private Rigidbody2D _rigidbody;
    private AudioSource samuraiAudioSource;
    void Start()
    {
        og_x = transform.position.x;
        set_speed = speed;
        samuraiAudioSource = gameObject.AddComponent<AudioSource>();
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
            direction.y = 0;
            if (direction.x > 0 && transform.position.x >= rightPoint.x)
            {
                speed = 0;
                animator.SetFloat("speed", 0);
            }
            else if (direction.x < 0 && transform.position.x <= leftPoint.x)
            {
                speed = 0;
                animator.SetFloat("speed", 0);
            }
            else
            {
                speed = set_speed;
            }
        }

        else
        {
            speed = set_speed;

            if (transform.position.x >= rightPoint.x)
            {
                direction = Vector2.left;
            }
            if (transform.position.x <= leftPoint.x)
            {
                direction = Vector2.right;
            }
        }

        if (direction.x * lastDir.x < 0)
            Flip();

        lastDir = direction;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = direction * speed;

        _timeToFire -= Time.deltaTime;

        animator.SetFloat("speed", _rigidbody.velocity.magnitude);

        if (playerInReach == true && _timeToFire <= 0f)
        {
            _timeToFire = attackTime;
            samuraiAudioSource.PlayOneShot(swordClip);
            animator.SetTrigger("attack");
        }
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            playerInReach = true;
        }

        if (other.gameObject.tag == "Fireball")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("destroy");
            Destroy(other.gameObject, .2f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            playerInReach = false;
        }

    }
}
