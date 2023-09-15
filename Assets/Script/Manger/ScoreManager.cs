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

    public int GetHitObjectCount()
    {
        int hitObjectCount = 0;

        // Scene ���� ��� GameObject�� �����ɴϴ�.
        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        // ��� GameObject�� ��ȸ�ϸ鼭 Hit ������Ʈ�� ���� ������Ʈ���� Ȯ���մϴ�.
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

