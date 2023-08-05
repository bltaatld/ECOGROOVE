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

        // objectPositions 배열을 objectsWithScript의 길이만큼 생성합니다.
        noteInfo.positionInfo = new Vector3[objectsWithScript.Length];
        noteInfo.spikePositionInfo = new Vector3[spike.Length];

        // 각 오브젝트의 위치를 objectPositions 배열에 저장합니다.
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

            // 이미 해당 위치에 노트가 있는지 확인합니다.
            foreach (Vector3 position in notePosition)
            {
                if (Vector3.Distance(position, targetParent.position) < 0.01f)
                {
                    // 이미 노트가 있으면 해당 위치의 노트를 삭제하고 notePosition에서 제거합니다.
                    foreach (Transform child in targetParent)
                    {
                        Destroy(child.gameObject);
                    }
                    RemoveNotePosition(targetParent.position);
                    return;
                }
            }

            // 노트를 인스턴스화하고, Hit 스크립트의 isEditMode 변수를 설정합니다.
            GameObject newNote = Instantiate(notePrefab, targetParent);
            Hit hitScript = newNote.GetComponent<Hit>();
            hitScript.isEditMode = true;

            // 노트의 위치를 타겟 오브젝트의 위치로 이동시킵니다.
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
                // 해당 위치를 notePosition에서 제거합니다.
                savePostion[i] = savePostion[savePostion.Length - 1];
                Array.Resize(ref savePostion, savePostion.Length - 1);
                return;
            }
        }
    }


    // notePosition에서 특정 위치의 노트 위치를 제거하는 함수
    private void RemoveNotePosition(Vector3 positionToRemove)
    {
        for (int i = 0; i < notePosition.Length; i++)
        {
            if (Vector3.Distance(notePosition[i], positionToRemove) < 0.01f)
            {
                // 해당 위치를 notePosition에서 제거합니다.
                notePosition[i] = notePosition[notePosition.Length - 1];
                Array.Resize(ref notePosition, notePosition.Length - 1);
                return;
            }
        }
    }

    // 버튼을 클릭했을 때 호출되는 함수
    public void OnButtonClick()
    {
        // EventSystem을 사용하여 현재 클릭된 버튼을 찾습니다.
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

        // 클릭된 버튼이 null이 아닌 경우 변수에 할당합니다.
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
