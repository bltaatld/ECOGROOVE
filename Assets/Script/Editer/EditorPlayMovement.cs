using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPlayMovement : MonoBehaviour
{
    public float speed;
    public RectTransform rectTransform;
    public Transform playerPos;
    public bool isPlayActive;
    private bool isStartActive;
    public Vector2 UIstartPos;
    public Vector2 playerStartPos;

    private void Start()
    {
        isStartActive = true;
    }

    public void PlayActive(bool boolean)
    {
        isPlayActive = boolean;
    }

    void Update()
    {
        if (isPlayActive)
        {
            StartPosSet();
            rectTransform.localPosition += Vector3.left * speed * Time.deltaTime;
        }
    }

    public void StartPosSet()
    {

        if (isStartActive)
        {
            UIstartPos = rectTransform.anchoredPosition;
            playerStartPos = playerPos.position;
            isStartActive = false;
        }
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = UIstartPos;
        playerPos.position = playerStartPos;
    }
}
