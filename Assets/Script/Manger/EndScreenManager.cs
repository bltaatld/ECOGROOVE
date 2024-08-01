using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndScreenManager : MonoBehaviour
{
    public ScoreManager score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI tierText;
    public GameObject objectSelf;
    public GameObject system;

    private void Update()
    {
        if (objectSelf.activeSelf == true)
        {
            system.SetActive(false);
            Time.timeScale = 0f;
            scoreText.text = score.currentScore.ToString();

            if (score.currentScore >= 1000000)
            {
                tierText.text = "S";
            }
            else if (score.currentScore >= 800000)
            {
                tierText.text = "A";
            }
            else if (score.currentScore >= 500000)
            {
                tierText.text = "B";
            }
            else
            {
                tierText.text = "C";
            }
        }

    }

    public void BackToMain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("StageSelect");
    }
}
