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

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentJumps = maxJump;
    }

    // Update is called once per frame
    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * speed;

        //transform.position += transform.right * xMovement;
        rigidBody.velocity = new Vector2(xMovement, rigidBody.velocity.y);

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

            Jump();
        }
    }

    void Jump()
    {

        rigidBody.AddForce(Vector2.up * jumpForce);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, (height / 2f) + 0.1f);

        return (hitInfo.collider != null);
    }
}
