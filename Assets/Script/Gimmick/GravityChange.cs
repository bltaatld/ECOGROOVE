using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour
{
    public GameObject player;

    public void ReverseGravity()
    {
        player.GetComponent<Rigidbody2D>().gravityScale *= -1f;

        // 플레이어의 스프라이트를 뒤집습니다.
        player.GetComponent<SpriteRenderer>().flipY = !player.GetComponent<SpriteRenderer>().flipY;
    }
}