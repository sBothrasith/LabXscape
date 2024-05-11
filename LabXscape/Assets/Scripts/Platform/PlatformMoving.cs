using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentPointIndex = 0;

    [SerializeField] private float speed = 1f;

	void FixedUpdate()
    {

        if (Vector2.Distance(waypoints[currentPointIndex].transform.position, transform.position) < .1f){
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                currentPointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentPointIndex].transform.position, Time.deltaTime * speed);
    }
        
}
