using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public GameObject[] objectUI;

    public List<string> collectedObject = new List<string>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        for(int i = 0; i < objectUI.Length; i++) {
            if (collision.gameObject.tag == objectUI[i].tag) {
                collision.gameObject.SetActive(false);
                objectUI[i].SetActive(true);
                collectedObject.Add(objectUI[i].tag);
            }
        }
    }
}
