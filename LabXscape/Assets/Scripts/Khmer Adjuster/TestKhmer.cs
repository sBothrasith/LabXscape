using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestKhmer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //string khmer = "ការ​ពង្រីក​នេះ​បាន​ផ្តល់​កន្លែង​សម្រាប់​បង្កើន​សកម្មភាព​ស្រាវជ្រាវ និង ការ​ផ្សាំង​សត្វទោច ក៏​ដូចជា ការ​ពង្រីក​វិសាលភាព​នៃ​ការ​ឃ្លាំ​មើល​ព្រៃ​របស់​ ចាហ៊ូ ផងដែរ​";
        string khmer = "សូមស្វាគមន៍មកកាន់ការបង្រៀន!";
        GetComponent<TextMeshProUGUI>().text = KhmerToKeyboard.ConvertKhmerToKeyboard(khmer);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
