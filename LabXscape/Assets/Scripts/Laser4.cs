using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser4 : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100f;
    public Transform laserFirePoint1;
    public Transform laserFirePoint2;
    public Transform laserFirePoint3;
    public Transform laserFirePoint4;
    public LineRenderer lineRenderer;
    Transform laserTransform;
    // Start is called before the first frame update
    private void Awake() {
        laserTransform = GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
