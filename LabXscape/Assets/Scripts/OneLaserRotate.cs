using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class OneLaserRotate : MonoBehaviour
{
    [SerializeField] private float buttonSpeed = 0.5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private float buttonCurrent, buttonTarget;


    public RotateLaser laserScript;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(transform.position.x, 0.25f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        buttonCurrent = Mathf.MoveTowards(buttonCurrent, buttonTarget, buttonSpeed * Time.deltaTime);
        if (transform.position == endPos) {
            buttonTarget = 0;
            laserScript.triggerLaser = true;
        }
        transform.position = Vector3.Lerp(startPos, endPos, buttonCurrent);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            buttonTarget = 1;
            if (laserScript.laserTarget == 1 && laserScript.triggerLaser) {
                laserScript.laserTarget = 0;
                laserScript.triggerLaser = false;
            }
            else if (laserScript.laserTarget == 0 && laserScript.triggerLaser) {
                laserScript.laserTarget = 1;
                laserScript.triggerLaser = false;
            }
        }
    }
}
