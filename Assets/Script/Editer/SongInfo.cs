using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Video;

public class SongInfo : MonoBehaviour
{
    [System.Serializable]
    public class Info
    {
        public string artist = "";
        public string song = "";

        [Space(10.0f)]
        public float bpm = 100.0f;
        public float offset = 0.0f;
        public float volume = 100.0f;   // Value Range : 0 ~ 100
        public float pitch = 100.0f;    // Value Range : 0 ~ 100

        [Space(10.0f)]
        public List<note> notes;

        [System.NonSerialized] public AudioClip songClip;

        public string ToJson()
        {
            return JsonUtility.ToJson(this, true);
        }
    }
}
