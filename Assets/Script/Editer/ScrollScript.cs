using Ozi_Story;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollScript : MonoBehaviour
{
    public static float WheelSpeed = 1.0f;
    public static float timing = 0.0f;
    public float Timing = 0.0f;

    public float Mul = 1.0f;
    public bool isNotAdd = false;

    public GameObject followObject;
    public TMP_InputField inputMul;

    private void Update()
    {
        Timing = timing;

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            Vector2 Whell = Input.mouseScrollDelta;
            transform.position += new Vector3(Whell.y * WheelSpeed * NoteWriter.MAX_ENLARGEMENT * Mul * Tap.canvasMul, 0.0f, 0.0f);


            // 스크롤 위로 올릴 때
            if (Whell.y > 0)
            {
                followObject.transform.position += new Vector3(-0.462963f, 0.0f, 0.0f);
                for (int i = 0; i < GameSystem.instance.noteManager.notePosition.Length; i++)
                {
                    GameSystem.instance.noteManager.notePosition[i] = new Vector3(GameSystem.instance.noteManager.notePosition[i].x + 50f, GameSystem.instance.noteManager.notePosition[i].y, GameSystem.instance.noteManager.notePosition[i].z);
                }
            }
            // 스크롤 아래로 내릴 때
            else if (Whell.y < 0)
            {
                followObject.transform.position += new Vector3(0.462963f, 0.0f, 0.0f);
                for (int i = 0; i < GameSystem.instance.noteManager.notePosition.Length; i++)
                {
                    GameSystem.instance.noteManager.notePosition[i] = new Vector3(GameSystem.instance.noteManager.notePosition[i].x - 50f, GameSystem.instance.noteManager.notePosition[i].y, GameSystem.instance.noteManager.notePosition[i].z);
                }
            }

            if (!isNotAdd) { timing -= Whell.y * WheelSpeed * (NoteWriter.MAX_ENLARGEMENT / NoteWriter.enlargement); }
        }
    }

    public void ResetCamPos()
    {
        followObject.transform.position = new Vector3(8.840002f, 1.37f, -10f);
    }

    public void Correction()
    {
        transform.position = new Vector2(transform.position.x, (140.0f - (timing * NoteWriter.enlargement)) * Mul * Tap.canvasMul);
    }

    public void SetMul()
    {
        if(float.TryParse(inputMul.text, out Mul))
        {
            Debug.Log("mul set to " + Mul.ToString());
        }
        else
        {
            Debug.Log("mul set failed");
        }
    }
}
