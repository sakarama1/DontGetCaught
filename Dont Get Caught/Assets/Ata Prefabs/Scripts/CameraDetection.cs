using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    public float angle;
    public float rayFrequency;
    public float angleIntervall;

    int intervallCount;
    int angleCount;
    
    // Start is called before the first frame update
    void Start()
    {
        intervallCount = (int)System.Math.Round(360 / angleIntervall);
        angleCount = (int)System.Math.Round(angle / rayFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<intervallCount; i++)
        {
            for(int j=0; i<angleCount; j++)
            {

            }
        }
    }
}
