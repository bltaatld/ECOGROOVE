using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FlipScreenItem : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float transitionDuration = 1.0f; // ��ȯ �ð�
    public bool isHit; // ��ȯ �ð�

    public Vector3 originalFollowOffset;
    public Vector3 targetFollowOffset;
    public float transitionEndTime; // ��ȯ ���� �ð�

    private void Start()
    {
        if (virtualCamera != null)
        {
            // Virtual Camera�� Follow Offset �ʱ� �� ��������
            originalFollowOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }

        // �ʱ� Ÿ�� Follow Offset ����
        targetFollowOffset = originalFollowOffset;
    }

    private void Update()
    {
        if (Time.time < transitionEndTime)
        {
            // ��ȯ ���� �� Lerp�� ����Ͽ� �ε巴�� ����
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