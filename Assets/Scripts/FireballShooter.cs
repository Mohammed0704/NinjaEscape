using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireballShooter : MonoBehaviour
{
    public GameObject fireball;
    public float fireballSpeed;
    public Animator animator;
    private Vector2 direction = Vector2.right;
    private Rigidbody2D _myRidigbody2D;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = fireball.GetComponent<SpriteRenderer>();
        _myRidigbody2D = GetComponent<Rigidbody2D>();
    }

    public void FireBullet()
    {
        if (GetComponent<PlayerController>().playerDirection == Vector2.left)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        GameObject g0 = Instantiate(fireball, transform.position,
        Quaternion.Euler(GetComponent<PlayerController>().playerDirection)) as GameObject;
        g0.GetComponent<Rigidbody2D>().velocity = GetComponent<PlayerController>().playerDirection * fireballSpeed;
        g0.transform.localEulerAngles = animator.transform.localEulerAngles;
    }
}

