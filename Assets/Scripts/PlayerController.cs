using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float playerSpeed = 10f;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2f;
    public float fireTime;
    public Vector2 playerDirection = Vector2.right;
    private Rigidbody2D playerRb;
    public Transform swordTransform; 
    public Collider2D swordCollider;
    private bool grounded = false;
    public bool doubleJumpAbility = false, specialAttackAbility = false;
    private float _timeToFire;
    private int amountOfAirJumps = 0, abilitySwitchIndex = 0;
    private SpriteRenderer spriteRenderer;

    //private float horizontal;
    private bool isFacingRight = true;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f; 
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;

    private Vector2 wallJumpingPower = new Vector2(4f, 8f);
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D playerCollider;

    public DoubleJumpButton doubleJumpButton;
    public FireballButton fireballButton;


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
        if (!animator.GetBool("crouch"))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerDirection = Vector2.left;
                playerRb.velocity = new Vector2(-playerSpeed, playerRb.velocity.y);
                spriteRenderer.flipX = true;
                swordTransform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                playerDirection = Vector2.right;
                playerRb.velocity = new Vector2(playerSpeed, playerRb.velocity.y);
                spriteRenderer.flipX = false;
                swordTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            }
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle between double jump and fireball abilities
            if (doubleJumpAbility)
            {
                doubleJumpAbility = false;
                specialAttackAbility = true;
            }
            else
            {
                doubleJumpAbility = true;
                specialAttackAbility = false;
            }

            // Update UI to show the current ability
            if (doubleJumpAbility)
            {
                doubleJumpButton.image.color = doubleJumpButton.activeColor;
                fireballButton.image.color = fireballButton.inactiveColor;
            }
            else if (specialAttackAbility)
            {
                doubleJumpButton.image.color = doubleJumpButton.inactiveColor;
                fireballButton.image.color = fireballButton.activeColor;
            }
        }
    }

    private void CheckInput()
    {
        KeyMove();
        KeyAbilities();

        
        if (!specialAttackAbility)
            KeyJump();

        if (!doubleJumpAbility)
            KeyAttack();
        

        KeyCrouch();

        // Adjust the collider if crouching
        if (animator.GetBool("crouch"))
        {
            playerCollider.size = new Vector2(playerCollider.size.x, 0.5f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, -0.25f);
        }
        else
        {
            playerCollider.size = new Vector2(playerCollider.size.x, 1f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, 0f);
        }
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
        playerCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // Checks all Inputs from the player.
        CheckInput();

        //Plays run animation depending on movement of player
        animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        
        _timeToFire -= Time.deltaTime;

        WallSlide();
        WallJump();

    }

    private void FixedUpdate()
    {
        if (playerRb.velocity.y < 0)
        {
            playerRb.gravityScale = fallMultiplier;
        } else if (playerRb.velocity.y > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.gravityScale = jumpMultiplier;
        }
        else
        {
            playerRb.gravityScale = 1f;
        }
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

    private bool IsWalled(){
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide(){
        if (IsWalled() && !grounded){
            isWallSliding = true;
            playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Clamp(playerRb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else{
            isWallSliding = false;
        }
    }

    /*
    private void Flip(){
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    */

    private void WallJump(){
        if (isWallSliding){
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else{
            wallJumpingCounter -= Time.deltaTime;
        }

        if((Input.GetKeyDown(KeyCode.UpArrow) && wallJumpingCounter > 0f)){
            isWallJumping = true;
            playerRb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection){
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping(){
        isWallJumping = false;
    }
}
