using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer MainMixer;
    public AudioMixer MusicMixer;
    public Slider Sensitivity;
    public Slider Volume;
    public Slider Music;
    private void Start()
    {
        Sensitivity.value = MainGameManager.Sensitivity;
        MainMixer.GetFloat("volume", out float volume);
        Volume.value = volume;
        MusicMixer.GetFloat("volume", out float music);
        Music.value = music;
    }
    public void SetVolume(float volume)
    {
        MainMixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        MusicMixer.SetFloat("volume", volume);
    }

    public void ChangeSensitivity(float sensitivity)
    {
        MainGameManager.Sensitivity = sensitivity;
    }
}
