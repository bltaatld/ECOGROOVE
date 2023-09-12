using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigntObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameSystem.instance.lightEffectManager.isLightNoteHit = true;
            Destroy(gameObject);
        }
    }
}
