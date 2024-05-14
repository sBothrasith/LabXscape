using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public CinemachineVirtualCamera playerCamera;
    public float startZoomSize;
    public float targetZoomSize;
    public float zoomSpeed;
    private float cameraCurrent;
    private float cameraTarget = 0;

    public RotateLaser[] laser;

    private float timeDelay = 0f;
    public float cameraDelayTime = 2f;


    // Update is called once per frame
    void Update()
    {
        bool zoomBack = laser.All(l => l.triggerLaser == true);
        cameraCurrent = Mathf.MoveTowards(cameraCurrent, cameraTarget, zoomSpeed * Time.deltaTime);
        if (zoomBack) {
            timeDelay += 1f * Time.deltaTime;
            if(timeDelay >= cameraDelayTime) {
                timeDelay= 0f;
                cameraTarget = 0;
            }
        }
		playerCamera.m_Lens.OrthographicSize = Mathf.Lerp(startZoomSize, targetZoomSize, cameraCurrent);

    }

	private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ButtonLaser") {
            cameraTarget = 1;
        }
    }
}
