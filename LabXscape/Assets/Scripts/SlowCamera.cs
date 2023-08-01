using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCamera : MonoBehaviour
{
    public float movingSpeed;
    public GameObject endingUI;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + movingSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        if(transform.position.x > 45) {
            endingUI.SetActive(true);
        }
    }
}
