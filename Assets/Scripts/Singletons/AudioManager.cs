using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Configuración del AudioManager")]
    public static AudioManager instance;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadAudioSettings(); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadAudioSettings()
    {
        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0.6f));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.6f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.6f));
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}