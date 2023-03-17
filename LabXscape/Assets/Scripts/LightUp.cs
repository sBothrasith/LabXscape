using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUp : MonoBehaviour
{
    public LineRenderer laser;
    private SpriteRenderer render;
    private Renderer ren;

    public float intensitySpeed;
    private float intensityStart = 0f;
    private float intensityEnd = 2f;

    private float intensityCurrent, intensityTarget;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        intensityCurrent = Mathf.MoveTowards(intensityCurrent, intensityTarget, intensitySpeed * Time.deltaTime);

        Vector3 endLaserPos = laser.GetPosition(1);
        float distance = Vector3.Distance(transform.position, endLaserPos);
        if(distance < 1.6f) {
            intensityTarget = 1;
        }
        else {
            intensityTarget = 0;
        }
        ren.material.SetFloat("_Intensity", Mathf.Lerp(intensityStart,intensityEnd,intensityCurrent));
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            if (ren.material.GetFloat("_Intensity") > 1.0f) {
                Debug.Log("kill");
            }
        }
    }
}
