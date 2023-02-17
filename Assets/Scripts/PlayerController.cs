using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float playerSpeed = 10f;
    public float jumpForce = 5f;
    private Rigidbody2D playerRb;
    private bool grounded = false;
    private SpriteRenderer spriteRenderer;

    private void KeyAttack()
    {
        //Adds attack one animation and checks if space bar is pressed 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("attack", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("attack", false);
        }
    }

    private void KeyMove()
    {
        //Moves player left and right and flips sprite depending on left or right arrow pressed
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * Time.deltaTime * playerSpeed;
            spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * Time.deltaTime * playerSpeed;
            spriteRenderer.flipX = false;
        }
    }

    private void KeyJump()
    {
        //Adds jump ability and plays jump animation
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            grounded = false;
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

    private void CheckInput()
    {
        KeyMove();
        KeyJump();
        KeyCrouch();
        KeyAttack();
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
    }
}
