using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class LongNoteSpawn : MonoBehaviour
{
    public List<GameObject> longNotes = new List<GameObject>();
    public Image imagePrefab;     // ���ἱ�� ����� �̹��� ������
    public Transform imageParent; // �̹������� �θ� ��ü
    public Transform pointA;      // ù ��° �̹��� ��ġ
    public Transform pointB;      // �� ��° �̹��� ��ġ

    public Image lineImage;      // ���ἱ �̹��� ������Ʈ
    private RectTransform lineTransform; // ���ἱ �̹����� RectTransform ������Ʈ

    public float currentLongWidth;

    public void RemoveGameObject(GameObject gameObjectToRemove)
    {
        int index = longNotes.IndexOf(gameObjectToRemove);

        if (index != -1)
        {
            longNotes.RemoveAt(index);

            // value �迭���� �ش� �ε����� ���� ����
            List<float> newValueList = new List<float>(GameSystem.instance.noteManager.longWidth);
            if (index < newValueList.Count)
            {
                newValueList.RemoveAt(index);
                GameSystem.instance.noteManager.longWidth = newValueList.ToArray();
            }
        }
    }

    public void LinkLongNote()
    {
        // ���ἱ �̹��� �������� �����Ͽ� ����
        lineImage = Instantiate(imagePrefab, imageParent);
        longNotes.Add(lineImage.gameObject);

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
