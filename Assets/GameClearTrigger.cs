using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearTrigger : MonoBehaviour
{
    public GameObject clearCanvas;
    public ScoreManager scoreManager;
    public SceneChanger sceneChanger;
    public PlayerInfo playerInfo;
    public int stage;
    public int currentBigCoin;

    private void Start()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StageData stageData = playerInfo.stageDataList.Find(data => data.stageNumber == stage);
            StageData nextStageData = playerInfo.stageDataList.Find(data => data.stageNumber == stage + 1);
            if (stageData != null)
            {
                nextStageData.isCleared = true;
                stageData.bigCoinsCollected = 1;
                stageData.score = scoreManager.currentScore;
            }

            playerInfo.SavePlayerInfoToJson();
            clearCanvas.SetActive(true);
        }
    }
}
