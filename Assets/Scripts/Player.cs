using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rigidBody;
    private SpriteRenderer sprite;
    public float jumpForce = 5f;
    public int maxJump = 1;
    public float height = 1.1f;
    public int currentJumps;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentJumps = maxJump;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;

        animator.SetFloat("xMove", Mathf.Abs(xMovement));

        //transform.position += transform.right * xMovement;
        rigidBody.velocity = new Vector2(xMovement, rigidBody.velocity.y);

       /*Handling animation through code instead of the animation system
        if(rigidBody.velocity.x != 0)
        {
            animator.Play("PlayerWalk");
        }
        else
        {
            animator.Play("PlayerIdle");
        }
       */
        //sprite.flipX = rigidBody.velocity.x < 0;
        if(rigidBody.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        if(rigidBody.velocity.x < 0)
        {
            sprite.flipX = true;
        }
       


        if(Input.GetButtonDown("Jump"))
        {
            if(IsGrounded())
            {
                currentJumps = maxJump;
            }
            if(currentJumps > 0)
            {
                Jump();
            }
            
        }
    }

    void Jump()
    {
        currentJumps--;
        animator.SetTrigger("Jump");
        //rigidBody.AddForce(Vector2.up * jumpForce);
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f);
        bool grounded = (hitInfo.collider != null);
        animator.SetBool("IsGrounded", grounded);
        return grounded;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        IsGrounded();
    }
}
