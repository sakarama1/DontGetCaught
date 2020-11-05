using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(0, 10)]
    public float zoom;

    private float height = 1.7f;

    private void LateUpdate()
    {
        transform.position = target.position - offset * zoom;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
