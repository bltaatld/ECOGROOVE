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

    // 게임 시작 시 최대 상한 점수를 노트 수로 나누어 계산
    private void Start()
    {
        float initialScore = (float)maxScore / (float)noteCount;
        Debug.Log("Initial Score: " + initialScore);
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

