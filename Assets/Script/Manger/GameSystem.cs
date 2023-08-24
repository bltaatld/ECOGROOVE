using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;
    public Timing timing;
    public PlayerControl playerControl;
    public NoteManager noteManager;
    public PlayerMovement playerMovement;
    public NoteInfoSaver noteInfoSaver;
    public KeyInteraction keyInteraction;

    public GameObject normalNote;
    public GameObject jumpNote;

    public void SwapToNormal()
    {
        noteManager.notePrefab = normalNote;
    }

    public void SwapToJump()
    {
        noteManager.notePrefab = jumpNote;
    }

    public void FindSongName()
    {
        noteManager.nameInput = GameObject.Find("SongName").GetComponent<TMP_InputField>();
    }

    public void Awake()
    {
        instance = this;
    }
}
