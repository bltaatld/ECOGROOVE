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
    public Vector3[] positionInfo;
    public Vector3[] spikePositionInfo;
}

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab;
    public Transform targetParent;
    public Transform spikeParent;
    public GameObject clickedButton;
    public NoteInfo noteInfo;
    public Vector3[] notePosition;
    public Vector3[] savePostion;

    public void AllSave()
    {
        GameObject[] objectsWithScript = GameObject.FindGameObjectsWithTag("note");
        GameObject[] spike = GameObject.FindGameObjectsWithTag("Damaged");

        // objectPositions �迭�� objectsWithScript�� ���̸�ŭ �����մϴ�.
        noteInfo.positionInfo = new Vector3[objectsWithScript.Length];
        noteInfo.spikePositionInfo = new Vector3[spike.Length];

        // �� ������Ʈ�� ��ġ�� objectPositions �迭�� �����մϴ�.
        for (int i = 0; i < spike.Length; i++)
        {
            noteInfo.positionInfo[i] = objectsWithScript[i].transform.position;
            noteInfo.spikePositionInfo[i] = spike[i].transform.position;
        }
    }


    public void NoteSpawn()
    {
        if (!notePrefab.GetComponent<Spike>())
        {
            targetParent = clickedButton.transform;

            // �̹� �ش� ��ġ�� ��Ʈ�� �ִ��� Ȯ���մϴ�.
            foreach (Vector3 position in notePosition)
            {
                if (Vector3.Distance(position, targetParent.position) < 0.01f)
                {
                    // �̹� ��Ʈ�� ������ �ش� ��ġ�� ��Ʈ�� �����ϰ� notePosition���� �����մϴ�.
                    foreach (Transform child in targetParent)
                    {
                        Destroy(child.gameObject);
                    }
                    RemoveNotePosition(targetParent.position);
                    return;
                }
            }

            // ��Ʈ�� �ν��Ͻ�ȭ�ϰ�, Hit ��ũ��Ʈ�� isEditMode ������ �����մϴ�.
            GameObject newNote = Instantiate(notePrefab, targetParent);
            Hit hitScript = newNote.GetComponent<Hit>();
            hitScript.isEditMode = true;

            // ��Ʈ�� ��ġ�� Ÿ�� ������Ʈ�� ��ġ�� �̵���ŵ�ϴ�.
            newNote.transform.position = targetParent.position;
        }

        if (notePrefab.GetComponent<Spike>())
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(clickedButton.transform.position);

            foreach (Vector3 position in savePostion)
            {
                if (Vector3.Distance(position, worldPosition) < 0.01f)
                {
                    foreach (Transform child in spikeParent)
                    {
                        Destroy(child.gameObject);
                    }
                    RemoveSpikePosition(worldPosition);
                    return;
                }
            }

            GameObject newNote = Instantiate(notePrefab,spikeParent);
            newNote.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
            Hit hitScript = newNote.GetComponent<Hit>();
            hitScript.isEditMode = true;
        }
    }

    private void RemoveSpikePosition(Vector3 positionToRemove)
    {
        for (int i = 0; i < savePostion.Length; i++)
        {
            if (Vector3.Distance(savePostion[i], positionToRemove) < 0.01f)
            {
                // �ش� ��ġ�� notePosition���� �����մϴ�.
                savePostion[i] = savePostion[savePostion.Length - 1];
                Array.Resize(ref savePostion, savePostion.Length - 1);
                return;
            }
        }
    }


    // notePosition���� Ư�� ��ġ�� ��Ʈ ��ġ�� �����ϴ� �Լ�
    private void RemoveNotePosition(Vector3 positionToRemove)
    {
        for (int i = 0; i < notePosition.Length; i++)
        {
            if (Vector3.Distance(notePosition[i], positionToRemove) < 0.01f)
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
            Debug.Log("Clicked Button: " + clickedButton.name);
        }
        else
        {
            Debug.Log("No Button Clicked or Button Component not found.");
        }
    }
}
