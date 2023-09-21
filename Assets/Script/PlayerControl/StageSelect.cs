using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public TextMeshProUGUI stageInfo;
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI stageScore;
    public Button[] stageSelectButton;
    public Image stageImage;
    public GameObject[] bigCoinImage;

    public void Start()
    {
        for(int i = 0; i < stageSelectButton.Length; i++)
        {
            stageSelectButton[i].gameObject.SetActive(GameSystem.instance.playerInfo.stageDataList[i].isCleared);
        }
    }

    public void bigCoinChange(int type)
    {
        for (int i = 0; i < GameSystem.instance.playerInfo.stageDataList[type - 1].bigCoinsCollected; i++)
        {
            bigCoinImage[i].SetActive(true);
        }    
    }

    public void scoreChange(int type)
    {
        stageScore.text = GameSystem.instance.playerInfo.stageDataList[type - 1].score.ToString();
    }

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
