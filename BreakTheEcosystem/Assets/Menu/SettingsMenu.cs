using BTE.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider Sensitivity;
    private void Start()
    {
        Sensitivity.value = MainGameManager.Sensitivity;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void ChangeSensitivity(float sensitivity)
    {
        MainGameManager.Sensitivity = sensitivity;
    }
}
