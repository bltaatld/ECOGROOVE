using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScene : MonoBehaviour
{
    public SceneChanger sceneChanger;
    public string stageName;

    public void SetStagename(string stname)
    {
        stageName = stname;
    }

    public void StageChange()
    {
        sceneChanger.SceneChange(stageName);
    }
}
