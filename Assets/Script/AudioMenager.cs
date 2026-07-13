using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenager : MonoBehaviour
{
    [Header("------ Audio Source ------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("------ Audio Clip ------")]
    public AudioClip background;

    [SerializeField] private UnityEngine.Audio.AudioMixer myMixer;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        // Rilegge e applica il volume salvato
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        }
    }
}
