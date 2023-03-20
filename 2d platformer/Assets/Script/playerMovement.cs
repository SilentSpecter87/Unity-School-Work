using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // RigidBody
    private Rigidbody2D rb;
    private int jumpcount;
    public int maxJumpCount;
    public float moveDirection;
    public float movespeed;
    private bool isJumping;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform GroundCheck;
    public LayerMask groundObjects;
    private bool facingRight = true;
    private bool isGrounded;
    public float checkRadius;
    Animator myAnimator;
    
    private void Awake()
    {
        // gets rigidbody componet on the gameobject
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        jumpcount = maxJumpCount;
    }
    private void ProccessInputs()
    {
        //Get Inputs
        moveDirection = Input.GetAxis("Horizontal"); // -1 and 1
        if (Input.GetButtonDown("Jump") && jumpcount > 0)
        {
            isJumping = true;
        }

    }
    void Update()
    {
        ProccessInputs();
        Animate();

    }
    private void FixedUpdate()
    {
        //Check if grounded
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpcount = maxJumpCount;
        }
        //move
        MoveRB();
    }
    private void MoveRB()
    {

        rb.velocity = new Vector2(moveDirection * movespeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpcount--;
        }
        isJumping = false;
        // Player running animation
        if (moveDirection!=0)
        {
            myAnimator.SetBool("Running", true); 
        }
        else
        {
            myAnimator.SetBool("Running", false);
        }
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    // Update is called once per frame

    private void FlipCharacter()
    {
        facingRight = !facingRight; //inverse bool
        transform.Rotate(0f, 180f, 0f);
    }
    IEnumerator PowerUpSpeed()
    {
        movespeed = 9;
        yield return new WaitForSeconds(5);
        movespeed = 5;
    }
    public void SpeedPowerUp()
    {
        StartCoroutine(PowerUpSpeed());
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Moving Platform")
        {
            transform.parent = col.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Moving Platform")
        {
            transform.parent = null;
        }
    }
}
