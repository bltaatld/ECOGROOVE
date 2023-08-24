using Ozi_Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeatLine : MonoBehaviour
{
    public GameObject line;
    public ScrollScript script;
    public float bpm;
    public static int offset = 0;
    public static float distance = 1.0f;

    public static float mul = 1.0f;
    public static bool isSpawn = false;

    public TMP_InputField bpmInput;

    private GameObject root;

    public void SpawnLine(float count = 1000)
    {
        if (!isSpawn)
        {
            script.Correction();

            root = new GameObject("Beat Line");
            root.transform.parent = transform;

            float ms = (60.0f / bpm) * 100.0f * distance;
            for (int i = 0; i < count; i++)
            {
                float result = (ms * i) + offset;
                GameObject @object = Instantiate(line, root.transform);
                @object.transform.position = new Vector2(transform.position.y + (result * NoteWriter.enlargement * mul * Tap.canvasMul), 425f);
                @object.transform.localScale = new Vector2(Tap.canvasMul, Tap.canvasMul);
                @object.transform.GetChild(1).GetComponent<Text>().text = $"{result}ms";
            }

            root.transform.position = new Vector2(0, 0);
            isSpawn = true;
        }
    }
    public void UpdateLine()
    {
        if (isSpawn)
        {
            DeleteLine();
            SpawnLine();
        }
    }
    public void DeleteLine()
    {
        if (isSpawn)
        {
            DestroyImmediate(root);
            isSpawn = false;
        }
    }

    public void SetBPM()
    {
        if(float.TryParse(bpmInput.text, out bpm))
        {
            Debug.Log("bpm set to " + bpm.ToString());
        }
        else
        {
            Debug.Log("bpm set failed");
            bpm = 1f;
        }
    }
}
