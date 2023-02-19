using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float playerSpeed = 10f;
    public float jumpForce = 5f;
    public float fireTime;
    public Vector2 playerDirection = Vector2.right;
    private Rigidbody2D playerRb;
    public Transform swordTransform; 
    public Collider2D swordCollider;
    private bool grounded = false;
    private bool doubleJumpAbility = false, specialAttackAbility = false;
    private float _timeToFire;
    private int amountOfAirJumps = 0;
    private SpriteRenderer spriteRenderer;

    private bool checkIfGroundedOrDoubleJump()
    {
        return grounded || (doubleJumpAbility && !(amountOfAirJumps >= 2));
    }

    private void KeyAttack()
    {
        //Adds attack one animation and checks if space bar is pressed 
        if (!specialAttackAbility)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("attack", true);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                animator.SetBool("attack", false);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space) && _timeToFire <= 0f)
            {
                animator.SetTrigger("fireball");
                _timeToFire = fireTime;
            }
        }
    }

    private void KeyMove()
    {
        //Moves player left and right and flips sprite depending on left or right arrow pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerDirection = Vector2.left;
            transform.position += Vector3.left * Time.deltaTime * playerSpeed;
            spriteRenderer.flipX = true;
            swordTransform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerDirection = Vector2.right;
            transform.position += Vector3.right * Time.deltaTime * playerSpeed;
            spriteRenderer.flipX = false;
            swordTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void KeyJump()
    {
        //Adds jump ability and plays jump animation
        if (Input.GetKeyDown(KeyCode.UpArrow) && checkIfGroundedOrDoubleJump())
        {
            grounded = false;
            amountOfAirJumps++;
            playerRb.velocity = new Vector2(0, jumpForce);
            animator.SetBool("jump", true);
        }
    }

    private void KeyCrouch()
    {
        //Adds crouch animation
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("crouch", true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("crouch", false);
        }
    }

    private void KeyAbilities()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            doubleJumpAbility = !doubleJumpAbility;
        }
        else if (Input.GetKeyUp(KeyCode.X))
        {
            specialAttackAbility = !specialAttackAbility;
        }
    }

    private void CheckInput()
    {
        KeyMove();
        KeyAbilities();
        if (!specialAttackAbility)
            KeyJump();
        KeyCrouch();
        if (!doubleJumpAbility)
            KeyAttack();
    }

    private void Attack()
    {
        // Detect if the virtual sword collider overlaps with any enemy colliders
        Collider2D hit = Physics2D.OverlapBox(swordCollider.bounds.center, swordCollider.bounds.size, 0f);
        if (hit.CompareTag("Enemy"))
        {
            Destroy(hit.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks all Inputs from the player.
        CheckInput();

        //Plays run animation depending on movement of player
        animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        
        _timeToFire -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "walkable-ground" && !grounded)
        {
            grounded = true;
            animator.SetBool("jump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        amountOfAirJumps = 0;
    }
}
