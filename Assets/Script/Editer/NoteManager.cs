using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System;

[System.Serializable]
public class NoteInfo
{
    public string songName;
    public Vector3[] positionInfo;
    public Vector3[] spikePositionInfo;
}

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform targetParent;
    public GameObject clickedButton;
    public NoteInfo noteInfo;
    public Vector3[] notePosition;
    public Vector3[] savePostion;

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

    public void AllSave(string fileName)
    {
        GameObject[] objectsWithScript = GameObject.FindGameObjectsWithTag("note");
        GameObject[] spike = GameObject.FindGameObjectsWithTag("Damaged");

        // objectPositions �迭�� objectsWithScript�� ���̸�ŭ �����մϴ�.
        noteInfo.positionInfo = new Vector3[objectsWithScript.Length];
        noteInfo.spikePositionInfo = new Vector3[spike.Length];

        // �� ������Ʈ�� ��ġ�� objectPositions �迭�� �����մϴ�.
        for (int i = 0; i < objectsWithScript.Length; i++)
        {
            noteInfo.positionInfo[i] = objectsWithScript[i].transform.position;
        }

        for (int i = 0; i < spike.Length; i++)
        {
            noteInfo.spikePositionInfo[i] = spike[i].transform.position;
        }

        GameSystem.instance.noteInfoSaver.SaveData(noteInfo, fileName + ".json");
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
        else
        {
            targetParent = clickedButton.transform;

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
        }
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
