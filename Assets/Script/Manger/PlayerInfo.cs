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
            // ����: �ʱ�ȭ
            playerData = new Player();
            skinData = new Skin();

            // ����: �÷��̾� ������ ����
            playerData.life = 3;
            playerData.coin = 100;

            // ����: ��Ų ������ ����
            skinData.skin1 = true;
            skinData.skin2 = false;

            // ����: �������� ������ ����
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
                stage1.isCleared = true; // �������� 1�� Ŭ�����ߴٰ� ����
                stage1.bigCoinsCollected = 0; // �������� 1���� 5���� �� ���� ȹ��
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
        Debug.Log(Application.persistentDataPath + "/" + "player_info.json" + "�� ������ ���� ��");
    }

    // JSON ���Ͽ��� �÷��̾� ���� �ҷ�����
    public void LoadPlayerInfoFromJson()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + "player_info.json"))
        {
            string jsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/" + "player_info.json");
            Debug.Log(Application.persistentDataPath + "/" + "player_info.json" + "�� ������ �ε� ��");
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
        else
        {
            // ����� JSON ������ ���� ��� �ʱ�ȭ �Ǵ� �⺻�� ����
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
