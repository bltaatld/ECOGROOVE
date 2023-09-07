using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    public void DeleteObject()
    {
        if (GameSystem.instance.isEditMode)
        {
            Destroy(gameObject);
        }
    }
}
