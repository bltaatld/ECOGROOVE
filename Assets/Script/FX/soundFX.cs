using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundFX : MonoBehaviour
{
    public AudioSource[] sound;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            sound[0].Play();
        }   
    }
}
