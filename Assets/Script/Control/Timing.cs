using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timing : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();
    public note NoteSC;
    public bool isHit;

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    EffectManager EffectMan;
    AudioManager Audioman;
    Vector2[] timingBoxs = null;
    // Start is called before the first frame update
    void Start()
    {
        EffectMan = FindObjectOfType<EffectManager>();
        Audioman = FindObjectOfType<AudioManager>();
        NoteSC = GetComponent<note>();
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2, Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void CheckTiming()
    {
        for(int i =0; i < boxNoteList.Count; i++)
        {
            isHit = false;
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x <timingBoxs.Length; x++)
            {
                if(timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y) 
                {
                    boxNoteList[i].GetComponent<Hit>().HideNote();
                    EffectMan.HeartBeatEffect();
                    boxNoteList.RemoveAt(i);
                    Debug.Log("Hit " + x);
                    if (x == 0)
                    {
                        EffectMan.PerfectHitEffect();
                    }
                    if (x == 1)
                    {
                        EffectMan.GoodHitEffect();
                    }
                    if (x == 2)
                    {
                        EffectMan.BadHitEffect();
                    }
                    return;
                }
            }
        }
        Debug.Log("Miss");
    }
}
