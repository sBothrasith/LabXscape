using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;
    public Transform laserFirePoint;
    public LineRenderer lineRenderer;
    Transform laserTransform;

    private void Awake() {
        laserTransform = GetComponent<Transform>();

    }
    void Update()
    {
        ShootLaser();

    }

    void ShootLaser() {
        if(Physics2D.Raycast(laserTransform.position, transform.right)) {
            
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
            
            DrawRay(laserFirePoint.position, hit.point);
        }
        else {
            DrawRay(laserFirePoint.position, laserFirePoint.transform.right * rayDistance);
        }
    }
    void DrawRay(Vector2 startPos, Vector2 endPos) {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

}
