using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    public float longNoteSpeed;

    void Update()
    {
        if (!GameSystem.instance.isEditMode)
        {
            transform.localPosition += Vector3.right * longNoteSpeed * Time.deltaTime;
        }
    }
}
