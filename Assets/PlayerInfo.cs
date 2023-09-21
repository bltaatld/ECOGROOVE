using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player
{
    public int life;
    public int coin;
}

[System.Serializable]
public class Skin
{
    public bool skin1;
    public bool skin2;
}

[System.Serializable]
public class StageData
{
    public int stageNumber;
    public bool isCleared;
    public int bigCoinsCollected;
    public int score;
}

public class PlayerInfo : MonoBehaviour
{
    public Player playerData;
    public Skin skinData;
    public List<StageData> stageDataList = new List<StageData>();

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (System.IO.File.Exists(Application.persistentDataPath + "/" + "player_info.json"))
        {
            LoadPlayerInfoFromJson();
        }

        if(!System.IO.File.Exists(Application.persistentDataPath + "/" + "player_info.json"))
        {
            // 예제: 초기화
            playerData = new Player();
            skinData = new Skin();

            // 예제: 플레이어 데이터 설정
            playerData.life = 3;
            playerData.coin = 100;

            // 예제: 스킨 데이터 설정
            skinData.skin1 = true;
            skinData.skin2 = false;

            // 예제: 스테이지 데이터 설정
            for (int i = 1; i <= 5; i++)
            {
                StageData stage = new StageData();
                stage.stageNumber = i;
                stage.isCleared = false;
                stage.bigCoinsCollected = 0;
                stage.score = 0;

                stageDataList.Add(stage);
            }

            StageData stage1 = stageDataList.Find(stage => stage.stageNumber == 1);
            if (stage1 != null)
            {
                stage1.isCleared = true; // 스테이지 1을 클리어했다고 가정
                stage1.bigCoinsCollected = 0; // 스테이지 1에서 5개의 빅 코인 획득
                stage1.score = 0;
            }

            SavePlayerInfoToJson();
        }
    }

    void OnApplicationQuit()
    {
        SavePlayerInfoToJson();
    }

    public void SavePlayerInfoToJson()
    {
        string jsonData = JsonUtility.ToJson(this, true);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + "player_info.json", jsonData);
        Debug.Log(Application.persistentDataPath + "/" + "player_info.json" + "에 데이터 저장 됨");
    }

    // JSON 파일에서 플레이어 정보 불러오기
    public void LoadPlayerInfoFromJson()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + "player_info.json"))
        {
            string jsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + "player_info.json");
            Debug.Log(Application.persistentDataPath + "/" + "player_info.json" + "의 데이터 로드 됨");
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
        else
        {
            // 저장된 JSON 파일이 없을 경우 초기화 또는 기본값 설정
            playerData = new Player();
            skinData = new Skin();
            stageDataList.Clear();
            for (int i = 1; i <= 15; i++)
            {
                StageData stage = new StageData();
                stage.stageNumber = i;
                stage.isCleared = false;
                stage.bigCoinsCollected = 0;
                stage.score = 0;

                stageDataList.Add(stage);
            }
        }
    }
}
