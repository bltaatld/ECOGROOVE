using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Timing TimingManager;
    void Start()
    {
        TimingManager = FindObjectOfType<Timing>();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            TimingManager.CheckTiming();
        }
    }
}
