using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour
{
    public Transform[] spinLaser;



    private Vector3 startPos;
    private Vector3 endPos;
    private float movingButtonDuration = 1.5f;
    private float buttonElapsedTime;
    private bool startMovingButton = false;

    private Vector3[] startRotatePos;
    private float rotatingLaserDuration = 1.5f;
    private float laserElapsedTime;
    private bool[] startRotatingLaser;
    // Start is called before the first frame update
    void Start()
    {
        startRotatingLaser = new bool[spinLaser.Length];
        startRotatePos = new Vector3[spinLaser.Length];

        startPos = transform.position;
        endPos = new Vector3(transform.position.x, 0.35f, transform.position.z);

        for(int i = 0; i < spinLaser.Length; i++) {
            startRotatePos[i] = spinLaser[i].transform.eulerAngles;
            startRotatingLaser[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startMovingButton) {
            MovingButton();
        }
        for(int i = 0; i < startRotatingLaser.Length; i++) {
            if (startRotatingLaser[i]) {
                RotateLaser();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            startMovingButton = true;
            for (int i = 0; i < startRotatingLaser.Length; i++) {
                startRotatingLaser[i] = true;
            }
        }
    }

    private void MovingButton() {
        buttonElapsedTime += Time.deltaTime;

        transform.position = Vector3.Lerp(startPos, endPos, buttonElapsedTime/movingButtonDuration);
        if(transform.position.y == endPos.y) {
            startMovingButton = false;
        }
    }

    private void RotateLaser() {
        laserElapsedTime += Time.deltaTime;

        for(int i = 0; i < spinLaser.Length; i++) {
            spinLaser[i].transform.rotation = Quaternion.Lerp(Quaternion.Euler(0.0f,0.0f,startRotatePos[i].z), Quaternion.Euler(0.0f, 0.0f, startRotatePos[i].z + 90.0f), laserElapsedTime/ rotatingLaserDuration);
            if (spinLaser[i].transform.eulerAngles.z == startRotatePos[i].z + 90.0f) {
                startRotatePos[i] = spinLaser[i].transform.eulerAngles;
                startRotatingLaser[i] = false;
            }
        }
    }
}
