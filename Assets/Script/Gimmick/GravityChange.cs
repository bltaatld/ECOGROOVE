using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer spriteRenderer;

    public void ReverseGravity()
    {
        player.GetComponent<Rigidbody2D>().gravityScale *= -1f;

        // �÷��̾��� ��������Ʈ�� �������ϴ�.
        player.GetComponent<SpriteRenderer>().flipY = !player.GetComponent<SpriteRenderer>().flipY;
        spriteRenderer.flipY = !spriteRenderer.flipY;
    }
}