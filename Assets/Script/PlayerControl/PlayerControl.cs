using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Timing TimingManager;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            TimingManager.CheckTiming();
        }
    }
}
