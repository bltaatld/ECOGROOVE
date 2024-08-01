using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject objectSelf;
    public GameObject system;
    public string currentScene;
    public AudioSource music;

    private void Update()
    {
        if(objectSelf.activeSelf == true)
        {
            system.SetActive(false);
            music.Pause();
            Time.timeScale = 0f;
        }

        if (objectSelf.activeSelf == false)
        {
            system.SetActive(true);
            music.Play();
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        system.SetActive(true);
        music.Play();
        Time.timeScale = 1f;
        objectSelf.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        objectSelf.SetActive(false);
        SceneManager.LoadScene(currentScene);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }
}
