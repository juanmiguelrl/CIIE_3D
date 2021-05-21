using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameOverMenu : MonoBehaviour
{
    public string menu;
    public static GameOverMenu Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Display()
    {
        gameObject.SetActive(true);
        GameSystem.Instance.StopTimer();
        Controller.Instance.DisplayCursor(true);
        //Time.timeScale = 0;
    }

    public void OpenEpisode()
    {
        if (LevelSelectionUI.Instance.IsEmpty())
            return;

        UIAudioPlayer.PlayPositive();
        gameObject.SetActive(false);
        LevelSelectionUI.Instance.DisplayEpisode();
    }

    public void RestartGame()
    {
        UIAudioPlayer.PlayPositive();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menu);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
