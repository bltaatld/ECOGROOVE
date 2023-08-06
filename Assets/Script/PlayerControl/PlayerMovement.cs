using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("[ PlayerValue ]")]
    public float moveSpeed;
    public float jumpForce;
    public float maxHealth;
    public float health;

    [Header("[ Component ]")]
    public Rigidbody2D rigid;
    public Animator animator;
    public Slider slider;

    void Update()
    {
        slider.maxValue = maxHealth;
        slider.value = health;
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
