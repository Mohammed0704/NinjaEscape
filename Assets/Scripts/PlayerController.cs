using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 10f;
    public float jumpForce = 5f;
    private Rigidbody2D playerRb;
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + Vector3.left * Time.deltaTime * playerSpeed;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + Vector3.right * Time.deltaTime * playerSpeed;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            grounded = false;
            playerRb.velocity = new Vector2(0, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "walkable-ground" && !grounded)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}
