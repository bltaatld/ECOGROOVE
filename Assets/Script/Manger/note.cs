using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class note : MonoBehaviour
{
    [Header("* System Value")]
    public int bpm = 0;
    public bool isNotHit;
    public GameObject noteManger;

    [Header("* Object Reference")]
    [SerializeField] GameObject goNote = null;
    [SerializeField] GameObject spike = null;
    [SerializeField] GameObject longNoteLine = null;
    [SerializeField] GameObject longNoteEnd = null;
    Timing TimingManager;

    [Header("* SongInfo Reference")]
    public string fileName;
    public string songName;
    public Vector3[] positionInfo;
    public Vector3[] spikePositionInfo;
    public Vector3[] longNotePositionInfo;
    public Vector3[] longNoteEndPositionInfo;
    public float[] longWidthInfo;

    void Start()
    {
        TimingManager = GetComponent<Timing>();
        GameSystem.instance.noteInfoSaver.LoadData(fileName+".json", ref songName, ref positionInfo, ref spikePositionInfo, ref longNotePositionInfo, ref longNoteEndPositionInfo, ref longWidthInfo);
        SavedNoteSpawn();
    }

    public void SavedNoteSpawn()
    {
        foreach (Vector3 position in positionInfo)
        {
            GameObject t_note = Instantiate(goNote, position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            t_note.GetComponent<RectTransform>().position = new Vector2(t_note.GetComponent<RectTransform>().position.x, t_note.GetComponent<RectTransform>().position.y);
            TimingManager.boxNoteList.Add(t_note);
        }

        foreach (Vector3 position in spikePositionInfo)
        {
            GameObject t_note = Instantiate(spike, position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
        }

        foreach (Vector3 position in longNoteEndPositionInfo)
        {
            GameObject t_note = Instantiate(longNoteEnd, position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
        }

        for (int i = 0; i < longNotePositionInfo.Length; i++)
        {
            Vector3 position = longNotePositionInfo[i];
            float width = longWidthInfo[i] + 30f; // 해당 위치에 대한 폭 정보 가져오기
            GameObject t_note = Instantiate(longNoteLine, position, Quaternion.identity);
            t_note.GetComponent<RectTransform>().sizeDelta = new Vector2(width, t_note.GetComponent<RectTransform>().sizeDelta.y);
            t_note.transform.SetParent(this.transform);
        }
        GameSystem.instance.scoreManager.noteCount = GameSystem.instance.scoreManager.GetHitObjectCount();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("note"))
        {
            Debug.Log("OutOfBounds");
            GameSystem.instance.playerMovement.health -= 10f;
            TimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            //SceneManager.LoadScene("GameOverCutScene");
        }
    }
}