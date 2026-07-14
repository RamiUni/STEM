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
    public AudioClip click;
    public AudioClip cAnswer;
    public AudioClip wAnswer;
    public AudioClip fixing;
    public AudioClip mixer;
    public AudioClip raccolta;
    public AudioClip robot;
    public AudioClip walk;

    [SerializeField] private UnityEngine.Audio.AudioMixer myMixer;

    /*private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        // Rilegge e applica il volume salvato
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        }
    }*/

    void Start()
    {
        musicSource.enabled = true;

        musicSource.clip = background;
        musicSource.Play();

        Debug.Log("musicSource: " + musicSource);
        Debug.Log("musicSource enabled: " + musicSource.enabled);
        Debug.Log("musicSource gameObject active: " + musicSource.gameObject.activeSelf);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        SFXSource.PlayOneShot(click);
    }
}
