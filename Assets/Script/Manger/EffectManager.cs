using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteAnim = null;
    [SerializeField] Animator Heart = null;
    string Perfecthit = "Phit";
    string Goodhit = "Ghit";
    string Badhit = "Bhit";
    string ToBadhit = "TBhit";
    string HeartBeat = "Hbeat";

    public void PerfectHitEffect()
    {
        noteAnim.SetTrigger(Perfecthit);
    }

    public void GoodHitEffect()
    {
        noteAnim.SetTrigger(Goodhit);
    }

    public void BadHitEffect()
    {
        noteAnim.SetTrigger(Badhit);
    }

    public void ToBadHitEffect()
    {
        noteAnim.SetTrigger(ToBadhit);
    }

    public void HeartBeatEffect()
    {
        Heart.SetTrigger(HeartBeat);
    }
}
