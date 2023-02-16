using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentPointIndex = 0;
   

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentPointIndex].transform.position, transform.position) < .1f)
        {
            currentPointIndex++;
            if (currentPointIndex >= waypoints.Length)
            {
                transform.localScale = new Vector3(-0.2532206f, transform.localScale.y,transform.localScale.z);
                currentPointIndex = 0;
            }
            else
            {
                transform.localScale = new Vector3(0.2532206f, transform.localScale.y, transform.localScale.z);
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentPointIndex].transform.position, Time.deltaTime * speed);
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.transform.position.x > transform.position.x)
        {
            collision.transform.SetParent(transform);
        }
        
    }

    private void onCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);

    }
}
