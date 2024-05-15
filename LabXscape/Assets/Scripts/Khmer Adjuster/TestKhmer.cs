using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestKhmer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string khmerConsonantCombinations = "ក្ក";

        GetComponent<TextMeshProUGUI>().text = "ក្ក  =>  " + KhmerFontAdjuster.Adjust(khmerConsonantCombinations);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
