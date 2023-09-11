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
        GameSystem.instance.scoreManager.CalculateScore("PURE");
        noteAnim.SetTrigger(Perfecthit);
    }

    public void GoodHitEffect()
    {
        GameSystem.instance.scoreManager.CalculateScore("NEAT");
        noteAnim.SetTrigger(Goodhit);
    }

    public void BadHitEffect()
    {
        GameSystem.instance.scoreManager.CalculateScore("CLEAN");
        noteAnim.SetTrigger(Badhit);
    }

    public void ToBadHitEffect()
    {
        GameSystem.instance.scoreManager.CalculateScore("DIRTY");
        noteAnim.SetTrigger(ToBadhit);
    }

    public void HeartBeatEffect()
    {
        Heart.SetTrigger(HeartBeat);
    }
}
