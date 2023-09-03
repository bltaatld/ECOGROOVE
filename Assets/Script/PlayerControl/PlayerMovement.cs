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

        // �ܼ��� ��ġ�� Ŭ�� ��
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            animator.SetTrigger("IsClick");
        }
        // �������� �ൿ ��
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
            // ���⿡ ���� ���� ������ �߰��� �� �ֽ��ϴ�.
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
