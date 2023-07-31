using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ozi_Story
{
    public class Tap : MonoBehaviour
    {
        // Top Toggle
        //public GameObject ToggleGroup;
        // Now Tap
        //public GameObject Parent;
        //public Toggle toggle;

        //Position control according to resolution
        public static float canvasMul;

        private void Awake()
        {
            canvasMul = Camera.main.pixelHeight / 1080.0f;
        }
        /*
        public void Open()
        {
            Transform trans = ToggleGroup.transform;

            if (toggle.isOn)
            {
                Parent.SetActive(true);
                trans.position = new Vector2(330.0f * canvasMul, trans.position.y);
            }
            else
            {
                Parent.SetActive(false);
                trans.position = new Vector2(30.0f * canvasMul, trans.position.y);

            }
        }*/
    }

}
