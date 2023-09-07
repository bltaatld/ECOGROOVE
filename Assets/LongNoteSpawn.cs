using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongNoteSpawn : MonoBehaviour
{
    public Image imagePrefab;     // 연결선에 사용할 이미지 프리팹
    public Transform imageParent; // 이미지들의 부모 객체
    public Transform pointA;      // 첫 번째 이미지 위치
    public Transform pointB;      // 두 번째 이미지 위치

    public Image lineImage;      // 연결선 이미지 컴포넌트
    private RectTransform lineTransform; // 연결선 이미지의 RectTransform 컴포넌트

    public float currentLongWidth;

    public void LinkLongNote()
    {
        // 연결선 이미지 프리팹을 복제하여 생성
        lineImage = Instantiate(imagePrefab, imageParent);

        // 연결선 이미지의 RectTransform 컴포넌트를 가져오기
        lineTransform = lineImage.GetComponent<RectTransform>();

        // 연결선 이미지의 위치 설정
        Vector3 positionA = pointA.position;
        Vector3 positionB = pointB.position;
        Vector3 linePosition = (positionA + positionB) / 2f; // 두 지점의 중간 위치
        lineTransform.position = linePosition;

        // 연결선 이미지의 회전 설정 (두 지점 사이의 각도 계산)
        float angle = Mathf.Atan2(positionB.y - positionA.y, positionB.x - positionA.x) * Mathf.Rad2Deg;
        lineTransform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 연결선 이미지의 크기 설정 (두 지점 사이의 거리 계산)
        float distance = Vector3.Distance(positionA, positionB);
        lineTransform.sizeDelta = new Vector2(distance, lineTransform.sizeDelta.y);

        currentLongWidth = lineTransform.sizeDelta.x;
    }
}
