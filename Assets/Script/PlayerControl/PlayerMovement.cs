using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("[ PlayerValue ]")]
    public float moveSpeed;
    public float jumpForce;
    public float maxHealth;
    public float health;
    public bool canJump;
    public bool gimmikActive;

    [Header("[ Component ]")]
    public Rigidbody2D rigid;
    public Animator animator;
    public Slider slider;
    public GravityChange gravityGimmik;

    [Header("[ Flick ]")]
    public Vector2 touchStartPos;
    public Vector2 touchEndPos;
    public float flickThreshold = 100.0f;
    public float minFlickSpeed = 500.0f;

    void Update()
    {
        slider.maxValue = maxHealth;
        slider.value = health;

        // 단순한 터치로 클릭 시
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            animator.SetTrigger("IsClick");
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                Vector2 flickDirection = touchEndPos - touchStartPos;
                float flickDistance = flickDirection.magnitude;
                float flickSpeed = flickDistance / touch.deltaTime;

                if (flickDistance > flickThreshold && flickSpeed > minFlickSpeed&& canJump == true)
                {
                    Jump();
                }
            }
        }

        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
    }

    void Jump()
    {
        if (gimmikActive) { gravityGimmik.ReverseGravity(); }

        animator.SetBool("IsJump", true);
        rigid.velocity = new Vector2(rigid.velocity.x, 0f);
        rigid.AddForce(new Vector2(0f, jumpForce * 1));
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            animator.SetBool("IsJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
}
