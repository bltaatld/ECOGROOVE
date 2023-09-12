using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int maxScore = 10000000; // �ִ� ���� ����
    public int noteCount = 0; // ���� ��Ʈ ��
    public int currentScore = 0; // ���� ����
    public TextMeshProUGUI[] scoreText;

    // ���� �̸��� �ش� ������ ���� ����� �����ϴ� ����(Dictionary)
    private Dictionary<string, float> judgmentCoefficients = new Dictionary<string, float>
    {
        { "PURE", 1.0f },
        { "NEAT", 0.8f },
        { "CLEAN", 0.6f },
        { "DIRTY", 0.3f },
        { "MESSED", 0.0f }
    };

    // ���� ���� �� �ִ� ���� ������ ��Ʈ ���� ������ ���
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

    // �� ������ ���� ������ ���� ���� �߿� ���
    public void CalculateScore(string judgmentName)
    {
        if (judgmentCoefficients.ContainsKey(judgmentName))
        {
            float coefficient = judgmentCoefficients[judgmentName];
            float score = (float)maxScore / (float)noteCount * coefficient;
            currentScore += Mathf.FloorToInt(score); ; // ���� ������ ���ϱ�
            Debug.Log("Current Score: " + currentScore);
        }
        else
        {
            Debug.LogWarning("Unknown judgment: " + judgmentName);
        }
    }
}

