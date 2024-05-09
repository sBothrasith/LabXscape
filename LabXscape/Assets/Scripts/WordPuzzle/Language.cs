using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    public Toggle englishToggle;
    public Toggle khmerToggle;

    void Start() {
        if (PlayerPrefs.GetString("language") == "english") {
            englishToggle.isOn = true;
            khmerToggle.isOn = false;
        }
        else {
            englishToggle.isOn = false;
            khmerToggle.isOn = true;
        }
    }
    public void SetEnglishLanguage() {
        PlayerPrefs.SetString("language", "english");
    }

    public void SetKhmerLanguage() {
        PlayerPrefs.SetString("language", "khmer");
    }
}
