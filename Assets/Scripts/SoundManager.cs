using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]AudioMixer mainAudioMixer;
    [SerializeField]AudioSource bgmAudioSource;
    [SerializeField]AudioSource clickAudioSource;
    [SerializeField]AudioSource extraAudioSource;
    [SerializeField]AudioClip cardFlipClip;
    [SerializeField]AudioClip cardMatchClip;
    [SerializeField]AudioClip cardWrongClip;

    #region Singleton
    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #endregion
    private void Start()
    {
        mainAudioMixer.SetFloat("MasterVolume", 0f);
        bgmAudioSource.Play();
    }
    public void PlayButtonClickSound()
    {
        clickAudioSource.Play();
    }

    public void ToggleSound(bool isOn)
    {
        mainAudioMixer.SetFloat("MasterVolume", isOn ? 0 : -100);
    }

    public void PlayCardFlipSound()
    {
        extraAudioSource.PlayOneShot(cardFlipClip);
    }
    public void CardMatchSound()
    {
        extraAudioSource.PlayOneShot(cardMatchClip);
    }
    public void CardWrongSound()
    {
        extraAudioSource.PlayOneShot(cardWrongClip);
    }
}
