using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume")) {
            LoadVolume();
        }
        else {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume() {
        float volume = musicSlider.value;
        musicMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume() {
        float volume = sfxSlider.value;
        musicMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetMusicVolume();
        SetSFXVolume();
    }

}
