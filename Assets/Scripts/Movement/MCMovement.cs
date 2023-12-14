using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MCMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private CapsuleCollider2D capscoll;
    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 2f;

    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    private enum MovementState { idle, running, jumping, falling, gettingup }

    /*private StartFirstBoss triggerfirstboss;*/

    private bool interacting;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        capscoll = GetComponent<CapsuleCollider2D>();

        Debug.Log("Game start");
    }

    // Update is called once per frame
    private void Update()
    {

        if (!interacting)
        {
            dirX = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            UpdateAnimationState();

           /* Debug.Log("Horizontal Velocity: " + rb.velocity.x);
            Debug.Log("Vertical Velocity: " + rb.velocity.y);*/

        }

    }


    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }


    void Run()
    {

        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        //myAnimator.SetBool("isWalking", playerHasHorizontalSpeed);
    }

    /*public void TriggeredFirstBoss()
    {
        anim.SetBool("firstboss", true);
    }*/

    public void NotRun()
    {
        anim.SetInteger("state", 0);
        rb.velocity = Vector2.zero;
    }

    public void ToggleInteraction()
    {
        interacting = !interacting;
    }

    private void UpdateAnimationState()
    {

        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (!IsGrounded())
        {
            state = rb.velocity.y > 1f ? MovementState.jumping : MovementState.falling;
        }

        if (rb.velocity.y > 1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    /*private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, jumpableGround);
        bool grounded = hit.collider != null;

        return grounded;
    }*/

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(capscoll.bounds.center, capscoll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
