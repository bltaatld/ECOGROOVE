using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour
{
    public bool isEnd;
    public GameObject tagetObject;
    public GameObject[] ableObject;
    public AudioSource audio;

    private void Update()
    {
        if(isEnd)
        {
            audio.Play();
            ableObject[0].SetActive(true);
            ableObject[1].SetActive(true);
            ableObject[2].SetActive(true);
            tagetObject.SetActive(false);
        }
    }
}
