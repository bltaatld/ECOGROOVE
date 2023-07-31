using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class note : MonoBehaviour
{
    public int bpm = 0;
    public bool isNotHit;
    int currentBPM;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteApper = null;
    [SerializeField] GameObject goNote = null;
    Timing TimingManager;

    void Start()
    {
        TimingManager = GetComponent<Timing>();
        currentBPM = bpm;
    }
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteApper.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            TimingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm;
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