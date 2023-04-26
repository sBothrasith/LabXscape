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
    public Toggle fullScreenToggle;
    public Toggle fullHDToggle;
    public Toggle hdToggle;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume")) {
            LoadVolume();
            LoadFullScreen();
            LoadResolution();
        }
        else {
            SetMusicVolume();
            SetSFXVolume();
            SetFullScreen(true);
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

    public void SetFullScreen(bool isFullScreen) {
        Debug.Log(isFullScreen);
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt("isFullScreen", Screen.fullScreen== true? 1 : 0);
    }

    private void LoadFullScreen() {
        Debug.Log(PlayerPrefs.GetInt("isFullScreen") == 1 ? true : false);
        fullScreenToggle.isOn = PlayerPrefs.GetInt("isFullScreen") == 1 ? true : false;
        SetFullScreen(PlayerPrefs.GetInt("isFullScreen") == 1 ? true : false);
    }

    public void SetFullHD() {
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        PlayerPrefs.SetString("resolution", "FullHD");
    }

    public void SetHD() {
        Screen.SetResolution(1280, 720, Screen.fullScreen);
        PlayerPrefs.SetString("resolution", "HD");
    }

    private void LoadResolution() {
        if(PlayerPrefs.GetString("resolution") == "FullHD") {
            fullHDToggle.isOn = true;
            hdToggle.isOn = false;
            SetFullHD();
        }
        else {
            fullHDToggle.isOn = false;
            hdToggle.isOn = true;  
            SetHD();
        }
    }
}
