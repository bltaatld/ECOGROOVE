using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public TextMeshProUGUI stageInfo;
    public TextMeshProUGUI stageName;
    public Image stageImage;

    public void imageChange(Sprite image)
    {
        stageImage.sprite = image;
    }

    public void textChange(string info)
    {
        stageInfo.text = info;
    }

    public void stageChange(string info)
    {
        stageName.text = info;
    }
}
