using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;
using TMPro;
using Unity.VisualScripting;

[System.Serializable]
public class NoteInfo
{
    public string songName;
    public Vector3[] positionInfo;
    public Vector3[] spikePositionInfo;
    public Vector3[] longNotePositionInfo;
    public Vector3[] longNoteEndPositionInfo;
    public float[] longWidthInfo;
}

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public GameObject longNotePrefab;
    public Transform targetParent;
    public GameObject clickedButton;
    public string fileName;
    public TMP_InputField nameInput;
    public NoteInfo noteInfo;
    public Vector3[] notePosition;
    public Vector3[] longNotePosition;
    public float[] longWidth;
    public bool isLongNote;
    public bool isLongNoteActive;

    public void Update()
    {
        Vector2 Whell = Input.mouseScrollDelta;

        // ��ũ�� ���� �ø� ��
        if (Whell.y > 0)
        {
            for (int i = 0; i < notePosition.Length; i++)
            {
                notePosition[i] = new Vector3(notePosition[i].x + 50f, notePosition[i].y, notePosition[i].z);
            }
        }
        // ��ũ�� �Ʒ��� ���� ��
        else if (Whell.y < 0)
        {
            for (int i = 0; i < notePosition.Length; i++)
            {
                notePosition[i] = new Vector3(notePosition[i].x - 50f, notePosition[i].y, notePosition[i].z);
            }
        }
    }

    public void SetFileName()
    {
        fileName = nameInput.text;
    }

    public void AllSave()
    {
        GameObject[] objectsWithScript = GameObject.FindGameObjectsWithTag("note");
        GameObject[] spike = GameObject.FindGameObjectsWithTag("Damaged");
        GameObject[] longNote = GameObject.FindGameObjectsWithTag("LongNote");
        GameObject[] longNoteEnd = GameObject.FindGameObjectsWithTag("LongNoteEnd");

        // objectPositions �迭�� objectsWithScript�� ���̸�ŭ �����մϴ�.
        noteInfo.positionInfo = new Vector3[objectsWithScript.Length];
        noteInfo.spikePositionInfo = new Vector3[spike.Length];
        noteInfo.longWidthInfo = new float[longNote.Length];
        noteInfo.longNotePositionInfo = new Vector3[longNote.Length];
        noteInfo.longNoteEndPositionInfo = new Vector3[longNoteEnd.Length];

        // �� ������Ʈ�� ��ġ�� objectPositions �迭�� �����մϴ�.
        for (int i = 0; i < objectsWithScript.Length; i++)
        {
            noteInfo.positionInfo[i] = objectsWithScript[i].transform.position;
        }

        for (int i = 0; i < spike.Length; i++)
        {
            noteInfo.spikePositionInfo[i] = spike[i].transform.position;
        }

        for (int i = 0; i < longNote.Length; i++)
        {
            noteInfo.longNotePositionInfo[i] = longNote[i].transform.position;
            noteInfo.longNoteEndPositionInfo[i] = longNoteEnd[i].transform.position;
            noteInfo.longWidthInfo[i] = longWidth[i];
        }

        GameSystem.instance.noteInfoSaver.SaveData(noteInfo, fileName + ".json");
        UnityEditor.AssetDatabase.Refresh();
    }


    public void NoteSpawn()
    {
        if (notePrefab.GetComponent<Spike>())
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickedButton.transform.position);

            GameObject spikeNote = Instantiate(notePrefab);
            spikeNote.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
            Hit hitScript = spikeNote.GetComponent<Hit>();
            hitScript.isEditMode = true;
        }

        if (isLongNoteActive)
        {
            if (GameSystem.instance.keyInteraction.isKeyClick)
            {
                targetParent = GameSystem.instance.keyInteraction.currentButton.transform;
            }

            else
            {
                targetParent = clickedButton.transform;
            }

            // ��Ʈ�� �ν��Ͻ�ȭ�ϰ�, Hit ��ũ��Ʈ�� isEditMode ������ �����մϴ�.
            GameObject newNote = Instantiate(longNotePrefab, targetParent);

            // ��Ʈ�� ��ġ�� Ÿ�� ������Ʈ�� ��ġ�� �̵���ŵ�ϴ�.
            newNote.transform.position = targetParent.position;
            GameSystem.instance.longNoteLink.pointB = newNote.transform;
            GameSystem.instance.longNoteLink.LinkLongNote();

            Array.Resize(ref longWidth, longWidth.Length + 1);
            longWidth[longWidth.Length - 1] = GameSystem.instance.longNoteLink.currentLongWidth;

            isLongNoteActive = false;
        }

        else
        {
            //TargetButton Setting
            if (GameSystem.instance.keyInteraction.isKeyClick)
            {
                targetParent = GameSystem.instance.keyInteraction.currentButton.transform;
            }

            else
            {
                targetParent = clickedButton.transform;
            }

            // parent ������Ʈ�� �ڽĵ��� ��� �����ɴϴ�.
            Transform[] children = targetParent.GetComponentsInChildren<Transform>();

            // parent ������Ʈ�� �ڽĵ� �߿��� Hit ������Ʈ�� ������ �ִ� ������Ʈ�� ã�Ƽ� �����մϴ�.
            foreach (Transform child in children)
            {
                Hit hitComponent = child.GetComponent<Hit>();
                if (hitComponent != null)
                {
                    Destroy(child.gameObject);
                    RemoveNotePosition(child.transform.position);
                    return; // �ϳ��� Hit ������Ʈ�� ������ �ڽ��� ã������ �Լ��� �����մϴ�.
                }
            }

            // ��Ʈ�� �ν��Ͻ�ȭ�ϰ�, Hit ��ũ��Ʈ�� isEditMode ������ �����մϴ�.
            GameObject newNote = Instantiate(notePrefab, targetParent);
            Hit hitScript = newNote.GetComponent<Hit>();
            hitScript.isEditMode = true;

            // ��Ʈ�� ��ġ�� Ÿ�� ������Ʈ�� ��ġ�� �̵���ŵ�ϴ�.
            newNote.transform.position = targetParent.position;

            Array.Resize(ref notePosition, notePosition.Length + 1);
            notePosition[notePosition.Length - 1] = newNote.transform.position;

            if (isLongNote)
            {
                GameSystem.instance.longNoteLink.pointA = newNote.transform;
                isLongNoteActive = true;
            }
        }
    }

    public void longNoteActive(bool value)
    {
        isLongNote = value;
    }


    // notePosition���� Ư�� ��ġ�� ��Ʈ ��ġ�� �����ϴ� �Լ�
    private void RemoveNotePosition(Vector3 positionToRemove)
    {
        for (int i = 0; i < notePosition.Length; i++)
        {
            if (notePosition[i] == positionToRemove)
            {
                // �ش� ��ġ�� notePosition���� �����մϴ�.
                notePosition[i] = notePosition[notePosition.Length - 1];
                Array.Resize(ref notePosition, notePosition.Length - 1);
                return;
            }
        }
    }

    // ��ư�� Ŭ������ �� ȣ��Ǵ� �Լ�
    public void OnButtonClick()
    {
        // EventSystem�� ����Ͽ� ���� Ŭ���� ��ư�� ã���ϴ�.
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        // Ŭ���� ��ư�� null�� �ƴ� ��� ������ �Ҵ��մϴ�.
        if (clickedObject != null && clickedObject.GetComponent<Button>() != null)
        {
            clickedButton = clickedObject;
            Debug.Log("Clicked Button Pos: " + clickedButton.transform.position);
        }
        else
        {
            Debug.Log("No Button Clicked or Button Component not found.");
        }
    }
}
