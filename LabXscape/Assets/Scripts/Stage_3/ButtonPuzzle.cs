using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    [SerializeField] private float buttonSpeed = 0.5f;
    [SerializeField] private float laserSpeed = 0.5f;

    public Transform[] spinLaser;

    private Vector3 startPos;
    private Vector3 endPos;

    private Vector3[] startRotatePos;
    private Vector3[] endRotatePos;

    private float buttonCurrent, buttonTarget;
    private float laserCurrent, laserTarget;

    private bool triggerLaser = true;
    // Start is called before the first frame update
    void Start()
    {
        startRotatePos = new Vector3[spinLaser.Length];
        endRotatePos = new Vector3[spinLaser.Length];

        startPos = transform.position;
        endPos = new Vector3(transform.position.x, 0.25f, transform.position.z);

        for(int i = 0; i < spinLaser.Length; i++) {
            startRotatePos[i] = spinLaser[i].transform.eulerAngles;
            endRotatePos[i] = new Vector3(startRotatePos[i].x, startRotatePos[i].y, startRotatePos[i].z+90);
        }
    }

    // Update is called once per frame
    void Update()
    {
        buttonCurrent = Mathf.MoveTowards(buttonCurrent, buttonTarget, buttonSpeed * Time.deltaTime);
        laserCurrent = Mathf.MoveTowards(laserCurrent, laserTarget, laserSpeed * Time.deltaTime);
        if (transform.position == endPos) {
            buttonTarget = 0;
            triggerLaser = true;
        }
        transform.position = Vector3.Lerp(startPos, endPos, buttonCurrent);

        for(int i = 0; i < spinLaser.Length; i++) {
            spinLaser[i].rotation = Quaternion.Lerp(Quaternion.Euler(startRotatePos[i]), Quaternion.Euler(endRotatePos[i]),laserCurrent);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            buttonTarget = 1;
            if(laserTarget == 1 && triggerLaser) {
                laserTarget = 0;
                triggerLaser = false;
            }else if(laserTarget == 0) {
                laserTarget = 1;
                triggerLaser= false;
            }
        }
    }

}
