using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetection : MonoBehaviour
{
    public float angle;
    public float rayIntervall;
    public float angleIntervall;
    public float viewDistance;
    public LayerMask layerMask;
    public LayerMask guardCallMask;

    int intervallCount;
    int angleCount;
    IEnumerator coroutine;
    
    // Start is called before the first frame update
    void Start()
    {
        intervallCount = (int)(360 / angleIntervall);
        angleCount = (int)(angle / rayIntervall);
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i=0; i<intervallCount; i++)
        {
            for(int j=1; j<=angleCount; j++)
            {
                RaycastHit ray;
                Vector3 angleVec = transform.InverseTransformDirection(Mathf.Cos(angleIntervall * i), Mathf.Sin(angleIntervall * i), 0f) * Mathf.Tan(angle) * ((float)j/(float)angleCount);
                if(Physics.Raycast(transform.position, (angleVec + transform.forward), out ray, viewDistance, layerMask))
                {
                    Debug.DrawRay(transform.position, (transform.forward + angleVec) * 5, Color.white);
                    if (ray.transform.gameObject.CompareTag("Player"))
                    {
                        int radius = 20;
                        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, guardCallMask, QueryTriggerInteraction.Ignore);
                        foreach (var hitCollider in hitColliders)
                        {
                            Debug.Log("Start");
                            coroutine = hitCollider.gameObject.GetComponent<EnemyController>().GoThroughTheAlertZone(ray.point, false);
                            StartCoroutine(coroutine);
                        }
                    }
                    

                }
            }
        }

    }

}
