using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOptionsController : MonoBehaviour
{
    public static bool isGamePaused = false;
    [Header("Movimiento del Panel de Opciones")]
    public RectTransform optionsPanel;
    public Vector2 hiddenPosition = new Vector2(-2000f, 0f);
    public Vector2 visiblePosition = Vector2.zero;
    public float smoothTime = 0.3f;

    private Vector2 velocity = Vector2.zero;
    private bool isOptionsVisible = false;

    [Header("Configuración de Brillo")]
    public Slider brightnessSlider;
    public Image filtroBrillo;

    [Header("Configuración de Audio")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    public SpawnerEnemies spawnerEnemies;
    public PlayerController cameraController; 

    void Start()
    {
        optionsPanel.anchoredPosition = hiddenPosition;

        float savedBrillo = PlayerPrefs.GetFloat("Brillo", 1f);
        brightnessSlider.value = savedBrillo;
        SetBrightness(savedBrillo);
        brightnessSlider.onValueChanged.AddListener(SetBrightness);

        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.6f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.6f);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    void Update()
    {
        Vector2 targetPosition = isOptionsVisible ? visiblePosition : hiddenPosition;
        optionsPanel.anchoredPosition = Vector2.SmoothDamp(optionsPanel.anchoredPosition, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
    }

    public void ShowPanelOptions()
    {
        optionsPanel.SetAsLastSibling();
        isOptionsVisible = !isOptionsVisible;
    }

    public void ShowPanelPause()
    {
        if (!isOptionsVisible)
        {
            isOptionsVisible = true;
            Time.timeScale = 0f;
            isGamePaused = true;
            if (spawnerEnemies != null)
            {
                spawnerEnemies.StopSpawning(); 
            }
            if (cameraController != null)
            {
                cameraController.enabled = false; 
            }
        }
    }

    public void ResumeGame()
    {
        if (isOptionsVisible)
        {
            isOptionsVisible = false;
            Time.timeScale = 1f; 
            isGamePaused = false;
            if (spawnerEnemies != null)
            {
                spawnerEnemies.StartSpawning(); 
            }
            if (cameraController != null)
            {
                cameraController.enabled = true; 
            }
        }
    }

    private void SetBrightness(float brillo)
    {
        Color color = filtroBrillo.color;
        color.a = 1f - brillo;
        filtroBrillo.color = color;

        PlayerPrefs.SetFloat("Brillo", brillo);
        PlayerPrefs.Save();
    }

    private void SetMasterVolume(float volume)
    {
        AudioManager.instance.SetMasterVolume(volume);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.instance.SetSFXVolume(volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        brightnessSlider.onValueChanged.RemoveListener(SetBrightness);
        masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
        musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}