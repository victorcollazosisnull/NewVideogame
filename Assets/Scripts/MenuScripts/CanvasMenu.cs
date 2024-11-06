using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public Button easyButton;    
    public Button normalButton; 
    public Button hardButton;    
    public Button playButton;
    private void Start()
    {
        playButton.interactable = false;

        easyButton.onClick.AddListener(OnEasyButtonClicked);
        normalButton.onClick.AddListener(OnNormalButtonClicked);
        hardButton.onClick.AddListener(OnHardButtonClicked);
    }
    private void OnEasyButtonClicked()
    {
        playButton.interactable = true; 
        PlayerPrefs.SetString("SelectedDifficulty", "Easy"); 
    }

    private void OnNormalButtonClicked()
    {
        playButton.interactable = true; 
        PlayerPrefs.SetString("SelectedDifficulty", "Normal"); 
    }

    private void OnHardButtonClicked()
    {
        playButton.interactable = true; 
        PlayerPrefs.SetString("SelectedDifficulty", "Hard"); 
    }
    public void OnPlayButtonClicked()
    {
        string difficulty = PlayerPrefs.GetString("SelectedDifficulty");
        switch (difficulty)
        {
            case "Easy":
                MusicManager.Instance.StopAllMusic();
                MusicManager.Instance.PlayGameMusic();
                SceneManager.LoadScene("GameEasy");
                break;
            case "Normal":
                MusicManager.Instance.StopAllMusic();
                MusicManager.Instance.PlayGameMusic();
                SceneManager.LoadScene("GameNormal");
                break;
            case "Hard":
                MusicManager.Instance.StopAllMusic();
                MusicManager.Instance.PlayGameMusic();
                SceneManager.LoadScene("GameHard");
                break;
            default:
                break;
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("GameNormal");
        MusicManager.Instance.StopAllMusic();
        MusicManager.Instance.PlayGameMusic();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void RetryGame()
    {
        MusicManager.Instance.StopAllMusic();
        SceneManager.LoadScene("GameNormal");
        MusicManager.Instance.PlayGameMusic();
    }
    public void GoToMenu()
    {
        MusicManager.Instance.StopAllMusic();
        SceneManager.LoadScene("Menu");
        MusicManager.Instance.PlayMenuMusic();
    }
}