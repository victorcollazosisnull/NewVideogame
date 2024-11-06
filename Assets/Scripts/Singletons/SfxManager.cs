using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance { get; private set; }
    public AudioMixerGroup sfxAudioMixerGroup;
    public AudioClip poof;
    public AudioClip gameOver;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxAudioMixerGroup;
    }
    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOver);
    }
}
