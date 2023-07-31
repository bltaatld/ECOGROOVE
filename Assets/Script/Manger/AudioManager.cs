using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource MyAudio;
    bool musicStart = false;
    public int AudioOrder;
    private void Start()
    {
        MyAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (musicStart == true)
        {
                if (AudioOrder == 1)
                {
                    MyAudio.Play();
                }
        }
    }

    public void MusicStart()
    {
        --AudioOrder;
        musicStart = true;
    }
}
