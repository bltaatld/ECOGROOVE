using Ozi_Story;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public static float WheelSpeed = 1.0f;
    public static float timing = 0.0f;
    public float Timing = 0.0f;

    public float Mul = 1.0f;
    public bool isNotAdd = false;

    private void Update()
    {
        Timing = timing;

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            Vector2 Whell = Input.mouseScrollDelta;
            transform.position += new Vector3(Whell.y * WheelSpeed * NoteWriter.MAX_ENLARGEMENT * Mul * Tap.canvasMul, 0.0f, 0.0f);

            // ��ũ�� ���� �ø� ��
            if (Whell.y > 0)
            {
                // "TargetObject" �±׸� ���� ������Ʈ�� ã�Ƽ� �̵�
                GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("Damaged");
                foreach (GameObject targetObject in targetObjects)
                {
                    targetObject.transform.position += new Vector3(0.462963f, 0.0f, 0.0f);
                }
            }
            // ��ũ�� �Ʒ��� ���� ��
            else if (Whell.y < 0)
            {
                // "TargetObject" �±׸� ���� ������Ʈ�� ã�Ƽ� �̵�
                GameObject[] targetObjects = GameObject.FindGameObjectsWithTag("Damaged");
                foreach (GameObject targetObject in targetObjects)
                {
                    targetObject.transform.position += new Vector3(-0.462963f, 0.0f, 0.0f);
                }
            }

            if (!isNotAdd) { timing -= Whell.y * WheelSpeed * (NoteWriter.MAX_ENLARGEMENT / NoteWriter.enlargement); }
        }
    }

    public void Correction()
    {
        transform.position = new Vector2(transform.position.x, (140.0f - (timing * NoteWriter.enlargement)) * Mul * Tap.canvasMul);
    }
}
