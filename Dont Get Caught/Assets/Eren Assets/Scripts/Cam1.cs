using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam1 : MonoBehaviour
{
    public Transform trackedObject;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - trackedObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = trackedObject.transform.position + offset;
    }
}
