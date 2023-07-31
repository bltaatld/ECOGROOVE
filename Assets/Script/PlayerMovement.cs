using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("[ PlayerValue ]")]
    public float moveSpeed;
    public float jumpForce;

    [Header("[ Component ]")]
    public Rigidbody2D rigid;
    public Animator animator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsClick");
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IsJump",true);
            Jump();
        }

        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
    }

    void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(new Vector2(0f, jumpForce * 1));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsJump", false);
        }
    }
}
