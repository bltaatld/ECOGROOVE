using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public bool isStart;
    public string Scenename;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        if (isStart)
        {
            SceneChange(Scenename);
        }
    }

    public void SceneChange(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }
}
