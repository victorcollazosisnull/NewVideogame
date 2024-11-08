using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public void GoMenu()
    {
        MusicManager.Instance.StopAllMusic();
        SceneManager.LoadScene("Menu");
        MusicManager.Instance.PlayMenuMusic();
        Time.timeScale = 1f;
        isGamePaused = false;
    }
}
