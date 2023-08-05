using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class note : MonoBehaviour
{
    public int bpm = 0;
    public bool isNotHit;
    public GameObject noteManger;
    int currentBPM;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteApper = null;
    [SerializeField] GameObject goNote = null;
    [SerializeField] GameObject spike = null;
    Timing TimingManager;

    void Start()
    {
        TimingManager = GetComponent<Timing>();
        currentBPM = bpm;

        SavedNoteSpawn();
    }

    public void SavedNoteSpawn()
    {
        foreach (Vector3 position in noteManger.GetComponent<NoteManager>().noteInfo.positionInfo)
        {
            GameObject t_note = Instantiate(goNote, position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            t_note.transform.position = new Vector2(t_note.transform.position.x, 430f);
            TimingManager.boxNoteList.Add(t_note);
        }

        foreach (Vector3 position in noteManger.GetComponent<NoteManager>().noteInfo.spikePositionInfo)
        {
            GameObject t_note = Instantiate(spike, position, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("note"))
        {
            Debug.Log("OutOfBounds");
            TimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            /*
            TimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            SceneManager.LoadScene("GameOverCutScene");*/
        }
    }
}