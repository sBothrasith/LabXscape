using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HoriMoving : MonoBehaviour
{
    // Start is called before the first frame update
    Transform platTransform;

    public Transform pointA;
    public Transform pointB;

    float movingCurrent;
    public float movingTarget;
    public float movingSpeed;

    void Start()
    {
        pointA = pointA.GetComponent<Transform>();
        pointB = pointB.GetComponent<Transform>();
        platTransform = GetComponent<Transform>();
        platTransform.position = new Vector3(pointB.position.x, pointB.position.y, pointB.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movingCurrent = Mathf.MoveTowards(movingCurrent, movingTarget, movingSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(pointB.position, pointA.position, movingCurrent);

    }
}
