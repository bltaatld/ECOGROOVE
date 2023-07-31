using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitpointSC : MonoBehaviour
{
    AudioSource myAudio;
    bool musicStart = false;
    public void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("note"))
            {
                myAudio.Play();
                musicStart = true;
            }
        }
    }
}
