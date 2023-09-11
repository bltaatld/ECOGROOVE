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

        // 스크롤 위로 올릴 때
        if (Whell.y > 0)
        {
            for (int i = 0; i < notePosition.Length; i++)
            {
                notePosition[i] = new Vector3(notePosition[i].x + 50f, notePosition[i].y, notePosition[i].z);
            }
        }
        // 스크롤 아래로 내릴 때
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

        // objectPositions 배열을 objectsWithScript의 길이만큼 생성합니다.
        noteInfo.positionInfo = new Vector3[objectsWithScript.Length];
        noteInfo.spikePositionInfo = new Vector3[spike.Length];
        noteInfo.longWidthInfo = new float[longNote.Length];
        noteInfo.longNotePositionInfo = new Vector3[longNote.Length];
        noteInfo.longNoteEndPositionInfo = new Vector3[longNoteEnd.Length];

        // 각 오브젝트의 위치를 objectPositions 배열에 저장합니다.
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

            // 노트를 인스턴스화하고, Hit 스크립트의 isEditMode 변수를 설정합니다.
            GameObject newNote = Instantiate(longNotePrefab, targetParent);

            // 노트의 위치를 타겟 오브젝트의 위치로 이동시킵니다.
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

            // parent 오브젝트의 자식들을 모두 가져옵니다.
            Transform[] children = targetParent.GetComponentsInChildren<Transform>();

            // parent 오브젝트의 자식들 중에서 Hit 컴포넌트를 가지고 있는 오브젝트를 찾아서 제거합니다.
            foreach (Transform child in children)
            {
                Hit hitComponent = child.GetComponent<Hit>();
                if (hitComponent != null)
                {
                    Destroy(child.gameObject);
                    RemoveNotePosition(child.transform.position);
                    return; // 하나의 Hit 컴포넌트를 가지는 자식을 찾았으면 함수를 종료합니다.
                }
            }

            // 노트를 인스턴스화하고, Hit 스크립트의 isEditMode 변수를 설정합니다.
            GameObject newNote = Instantiate(notePrefab, targetParent);
            Hit hitScript = newNote.GetComponent<Hit>();
            hitScript.isEditMode = true;

            // 노트의 위치를 타겟 오브젝트의 위치로 이동시킵니다.
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


    // notePosition에서 특정 위치의 노트 위치를 제거하는 함수
    private void RemoveNotePosition(Vector3 positionToRemove)
    {
        for (int i = 0; i < notePosition.Length; i++)
        {
            if (notePosition[i] == positionToRemove)
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
            Debug.Log("Clicked Button Pos: " + clickedButton.transform.position);
        }
        else
        {
            Debug.Log("No Button Clicked or Button Component not found.");
        }
    }
}
