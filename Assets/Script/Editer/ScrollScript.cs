using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ozi_Story
{
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

            if(!Input.GetKey(KeyCode.LeftControl)) {
                Vector2 Whell = Input.mouseScrollDelta;
                transform.position += new Vector3(Whell.y * WheelSpeed * NoteWriter.MAX_ENLARGEMENT * Mul * Tap.canvasMul,0.0f, 0.0f);
                if(!isNotAdd) { timing -= Whell.y * WheelSpeed * (NoteWriter.MAX_ENLARGEMENT / NoteWriter.enlargement); }
            }
        }

        public void Correction()
        {
            transform.position = new Vector2(transform.position.x, (140.0f - (timing * NoteWriter.enlargement)) * Mul * Tap.canvasMul);
        }

        
    }
}
