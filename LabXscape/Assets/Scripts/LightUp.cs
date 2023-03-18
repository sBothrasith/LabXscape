using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public LineRenderer[] laser;
    private SpriteRenderer render;
    private Renderer ren;

    public GameObject spawnpoint;

    public float intensitySpeed;
    private float intensityStart = 0f;
    private float intensityEnd = 2f;

    private float intensityCurrent, intensityTarget;

    private float timeDelay = 0f;
    public float deathTime = 0.3f;
    Vector3[] endLaserPos;
    float[] distance;
    
    bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        ren = GetComponent<Renderer>();

        endLaserPos = new Vector3[laser.Length];
        distance = new float[laser.Length];
    }

    // Update is called once per frame
    void Update()
    {
        intensityCurrent = Mathf.MoveTowards(intensityCurrent, intensityTarget, intensitySpeed * Time.deltaTime);
        for(int i = 0; i < laser.Length; i++) {
            endLaserPos[i] = laser[i].GetPosition(1);
        }
        for (int i = 0; i < laser.Length; i++) {
            distance[i] = Vector3.Distance(transform.position, endLaserPos[i]);
            if (distance[i] < 1.6f) {
                intensityTarget = 1;
                hit = true;
            }
            else if (distance[i] > 1.6f && hit) {
                hit = false;
            }
            else if (!hit) {
                intensityTarget = 0;
            }
        }
        ren.material.SetFloat("_Intensity", Mathf.Lerp(intensityStart,intensityEnd,intensityCurrent));
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (ren.material.GetFloat("_Intensity") > 1.0f) {
                timeDelay += 1f * Time.deltaTime;
                Debug.Log(timeDelay);
                if(timeDelay >= deathTime) {
                    collision.gameObject.transform.position = spawnpoint.transform.position;
                    timeDelay = 0f;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            timeDelay = 0f;
        }
    }
}
