using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public NavMeshAgent agent;
    public GameObject[] patrolPoints;
    
    public int pointCount;
    public bool alerted;

    // Start is called before the first frame update
    void Start()
    {
        pointCount = 0;
        alerted = false;
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (agent.remainingDistance == 0)
        {
            int oldPointCount = pointCount;
            pointCount = Random.Range(0, patrolPoints.Length - 1);
            
            while(pointCount == oldPointCount)
            {
                pointCount = Random.Range(0, patrolPoints.Length - 1);
            }
        }

        if (!alerted)
        {
            agent.SetDestination(patrolPoints[pointCount].transform.position);
        }
        


    }

    public void GoToAlertZone(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

}
