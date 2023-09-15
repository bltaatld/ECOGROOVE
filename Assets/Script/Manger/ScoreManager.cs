using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int maxScore = 10000000; // 최대 상한 점수
    public int noteCount = 0; // 맵의 노트 수
    public int currentScore = 0; // 현재 점수
    public TextMeshProUGUI[] scoreText;

    // 판정 이름과 해당 판정에 대한 계수를 저장하는 사전(Dictionary)
    private Dictionary<string, float> judgmentCoefficients = new Dictionary<string, float>
    {
        { "PURE", 1.0f },
        { "NEAT", 0.8f },
        { "CLEAN", 0.6f },
        { "DIRTY", 0.3f },
        { "MESSED", 0.0f }
    };

    public int GetHitObjectCount()
    {
        int hitObjectCount = 0;

        // Scene 내의 모든 GameObject를 가져옵니다.
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        // 모든 GameObject를 순회하면서 Hit 컴포넌트를 가진 오브젝트인지 확인합니다.
        foreach (GameObject obj in allGameObjects)
        {
            if (obj.GetComponent<Hit>() != null && obj.GetComponent<Hit>().isSpike == false)
            {
                hitObjectCount++;
            }

            if (obj.GetComponent<NoteMove>() != null)
            {
                hitObjectCount++;
            }
        }

        Debug.Log("Hit objects count: " + hitObjectCount);
        return hitObjectCount;
    }

    private void Update()
    {
        scoreText[0].text = currentScore.ToString();
        scoreText[1].text = currentScore.ToString();
    }

    // 각 판정에 따른 점수를 게임 진행 중에 계산
    public void CalculateScore(string judgmentName)
    {
        if (judgmentCoefficients.ContainsKey(judgmentName))
        {
            float coefficient = judgmentCoefficients[judgmentName];
            float score = (float)maxScore / (float)noteCount * coefficient;
            currentScore += Mathf.FloorToInt(score); ; // 현재 점수에 더하기
            Debug.Log("Current Score: " + currentScore);
        }
        else
        {
            Debug.LogWarning("Unknown judgment: " + judgmentName);
        }
    }
}

