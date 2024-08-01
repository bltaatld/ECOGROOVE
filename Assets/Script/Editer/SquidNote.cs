using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidNote : MonoBehaviour
{
    public GameObject inkImage;

    private void OnDestroy()
    {
        Instantiate(inkImage, transform.parent);
    }
}
