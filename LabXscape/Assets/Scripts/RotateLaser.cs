using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 0.5f;


    private Vector3 startRotatePos;
    private Vector3 endRotatePos;
    private float laserCurrent;
    public float laserTarget = 0;

    public bool triggerLaser = true;
    // Start is called before the first frame update
    void Start()
    {
        startRotatePos = transform.rotation.eulerAngles;
        endRotatePos = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
        Debug.Log(startRotatePos);
        Debug.Log(endRotatePos);
    }

    // Update is called once per frame
    void Update()
    {
        
        laserCurrent = Mathf.MoveTowards(laserCurrent, laserTarget, laserSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(Quaternion.Euler(startRotatePos), Quaternion.Euler(endRotatePos),laserCurrent);
    }
}
