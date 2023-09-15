using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followFlip : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = spriteRenderer.flipX;
        gameObject.GetComponent<SpriteRenderer>().flipY = spriteRenderer.flipY;
    }
}
