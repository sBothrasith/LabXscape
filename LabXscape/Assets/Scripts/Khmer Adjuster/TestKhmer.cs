﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestKhmer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string khmerConsonantCombinations = "ទុយោ";

        GetComponent<TextMeshProUGUI>().text = KhmerFontAdjuster.Adjust(khmerConsonantCombinations);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
