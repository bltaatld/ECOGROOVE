using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JupiterFX : MonoBehaviour
{
    Vector3 destination = new Vector3(-997, 384, 0);
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, 0.005f);
    }
}
