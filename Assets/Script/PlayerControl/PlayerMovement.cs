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

    [Header("[ Component ]")]
    public Rigidbody2D rigid;
    public Animator animator;
    public Slider slider;

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
        // 스와이프 행동 시
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                CheckForFlick();
            }
        }

        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
    }

    void CheckForFlick()
    {
        Vector2 flickDirection = touchEndPos - touchStartPos;
        float flickDistance = flickDirection.magnitude;
        float flickSpeed = flickDistance / Time.deltaTime;

        if (flickDistance > flickThreshold && flickSpeed > minFlickSpeed)
        {
            Debug.Log("Flick detected!");
            Jump();
            // 여기에 리듬 게임 로직을 추가할 수 있습니다.
        }
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
