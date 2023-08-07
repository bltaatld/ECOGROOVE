using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;
    public Timing timing;
    public PlayerControl playerControl;
    public NoteManager noteManager;
    public PlayerMovement playerMovement;
    public NoteInfoSaver noteInfoSaver;

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

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
