using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemover : MonoBehaviour
{
    public void DeleteObject()
    {
        if (GameSystem.instance.isEditMode)
        {
            if (gameObject.GetComponent<LongNote>())
            {
                GameSystem.instance.longNoteLink.RemoveGameObject(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
