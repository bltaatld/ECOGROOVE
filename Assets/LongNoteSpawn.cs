using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongNoteSpawn : MonoBehaviour
{
    public Image imagePrefab;     // ���ἱ�� ����� �̹��� ������
    public Transform imageParent; // �̹������� �θ� ��ü
    public Transform pointA;      // ù ��° �̹��� ��ġ
    public Transform pointB;      // �� ��° �̹��� ��ġ

    public Image lineImage;      // ���ἱ �̹��� ������Ʈ
    private RectTransform lineTransform; // ���ἱ �̹����� RectTransform ������Ʈ

    public float currentLongWidth;

    public void LinkLongNote()
    {
        // ���ἱ �̹��� �������� �����Ͽ� ����
        lineImage = Instantiate(imagePrefab, imageParent);

        // ���ἱ �̹����� RectTransform ������Ʈ�� ��������
        lineTransform = lineImage.GetComponent<RectTransform>();

        // ���ἱ �̹����� ��ġ ����
        Vector3 positionA = pointA.position;
        Vector3 positionB = pointB.position;
        Vector3 linePosition = (positionA + positionB) / 2f; // �� ������ �߰� ��ġ
        lineTransform.position = linePosition;

        // ���ἱ �̹����� ȸ�� ���� (�� ���� ������ ���� ���)
        float angle = Mathf.Atan2(positionB.y - positionA.y, positionB.x - positionA.x) * Mathf.Rad2Deg;
        lineTransform.rotation = Quaternion.Euler(0f, 0f, angle);

        // ���ἱ �̹����� ũ�� ���� (�� ���� ������ �Ÿ� ���)
        float distance = Vector3.Distance(positionA, positionB);
        lineTransform.sizeDelta = new Vector2(distance, lineTransform.sizeDelta.y);

        currentLongWidth = lineTransform.sizeDelta.x;
    }
}
