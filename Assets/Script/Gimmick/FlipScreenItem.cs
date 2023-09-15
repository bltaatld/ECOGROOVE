using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FlipScreenItem : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float transitionDuration = 1.0f; // 전환 시간
    public bool isHit; // 전환 시간

    public Vector3 originalFollowOffset;
    public Vector3 targetFollowOffset;
    public float transitionEndTime; // 전환 종료 시간

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Virtual Camera의 Follow Offset 초기 값 가져오기
            originalFollowOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }

        // 초기 타겟 Follow Offset 설정
        targetFollowOffset = originalFollowOffset;
    }

    private void Update()
    {
        if (Time.time < transitionEndTime)
        {
            // 전환 중일 때 Lerp를 사용하여 부드럽게 변경
            float t = (Time.time - (transitionEndTime - transitionDuration)) / transitionDuration;
            Vector3 newFollowOffset = Vector3.Lerp(originalFollowOffset, targetFollowOffset, t);
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = newFollowOffset;
            
            foreach (var flipScreenItem in FindObjectsOfType<FlipScreenItem>())
            {
                flipScreenItem.isHit = true;
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var flipScreenItem in FindObjectsOfType<FlipScreenItem>())
            {
                if (isHit)
                {
                    flipScreenItem.originalFollowOffset.x *= -1;
                    flipScreenItem.targetFollowOffset.x *= -1;
                }
                else
                {
                    flipScreenItem.targetFollowOffset.x *= -1;
                }
            }
            transitionEndTime = Time.time + transitionDuration;

            collision.gameObject.GetComponent<SpriteRenderer>().flipX = !collision.gameObject.GetComponent<SpriteRenderer>().flipX;
            GameSystem.instance.playerMovement.moveSpeed *= -1f;
            Destroy(gameObject,1f);
        }
    }
}