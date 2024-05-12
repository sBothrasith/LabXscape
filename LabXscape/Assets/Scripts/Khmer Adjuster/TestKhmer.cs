using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestKhmer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string khmerConsonantCombinations = "ក្ក,ក្ខ,ក្គ,ក្ឃ,ក្ង,ក្ច,ក្ឆ,ក្ជ,ក្ឈ,ក្ញ,ក្ដ,ក្ឋ,ក្ឌ,ក្ឍ,ក្ណ,ក្ត,ក្ថ,ក្ถ,ក្ទ,ក្ធ,ក្ន,ក្ប,ក្ផ,ក្ព,ក្ភ,ក្ម,ក្យ,ក្រ,ក្ល,ក្វ,ក្ស,ក្ហ,ក្ឡ,ក្អ, នំបុ័ង ខ្ញុំ កំពុង ជិះ កង់,,,,,  \u17D2_\u178B ប៉ា ";

        GetComponent<TextMeshProUGUI>().text = KhmerFontAdjuster.Adjust(khmerConsonantCombinations);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
