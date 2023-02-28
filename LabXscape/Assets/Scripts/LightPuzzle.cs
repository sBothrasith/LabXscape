using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    public LineRenderer light1;
    public LineRenderer light2;
    public int laserID;
    public Stage3Puzzle puzzle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        
    }

    void CheckCollision() {
        Vector3 lightPos1 = light1.GetPosition(1);
        Vector3 lightPos2 = light2.GetPosition(1);

        Vector3 lightHangPos = transform.position;

        float distance1 = Vector3.Distance(lightPos1, lightHangPos);
        float distance2 = Vector3.Distance(lightPos2, lightHangPos);
        if (distance1 < 0.3f || distance2 < 0.3f) {
            puzzle.setLightTrue(laserID);
        }
        else {
            puzzle.setLightFalse(laserID);
        }

    }
}
