using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    NavMeshAgent agent;
    Animator animator;

    public GameObject[] patrolPoints;
    public int pointCount;
    public bool alerted;
    public List<GameObject> guardsInDistance;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        guardsInDistance = new List<GameObject>();

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
    
    public void ShootPlayer(Transform playerTransform)
    {
        agent.isStopped = true;
        animator.SetBool("ShouldMove", false);
        transform.LookAt(playerTransform);
        animator.SetBool("Fire", true);
        
    }



    public void AlertObj(RaycastHit raycast)
    {
        alerted = true;
        GoToAlertZone(raycast.transform.position);
        for (int j = 0; j < guardsInDistance.Count; j++)
        {
            guardsInDistance[j].GetComponent<EnemyController>().alerted = true;
            guardsInDistance[j].GetComponent<EnemyController>().GoToAlertZone(raycast.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guard"))
            guardsInDistance.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Guard"))
            guardsInDistance.Remove(other.gameObject);
    }

}
