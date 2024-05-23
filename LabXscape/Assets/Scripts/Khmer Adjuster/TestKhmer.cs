using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestKhmer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string khmerConsonantCombinations = "ការ​ចំណាយ​ក្នុង​វិស័យ​នេះនឹង​បន្ត​កើន​បន្ថែម​ទៀត​ដើម្បី​បង្កើន​លទ្ធផល​សុខភាព ផ្សារ​​ភ្ជាប់​ជាមួយ​ប្រសិទ្ធិភាព និង​សក្ដិសិទ្ធ​ភាព​នៃ​ការ​ចំណាយ";
        GetComponent<TextMeshProUGUI>().text = KhmerFontAdjuster.AdjustVowel(KhmerFontAdjuster.Adjust(khmerConsonantCombinations));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
