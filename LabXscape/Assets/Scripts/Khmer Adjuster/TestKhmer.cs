using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestKhmer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var s = "ខ ្ រ      ខ្រ";

        GetComponent<TextMeshProUGUI>().text = KhmerFontAdjuster.Adjust(s);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
