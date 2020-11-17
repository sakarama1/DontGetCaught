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
    public GameObject bullet;
    public GameObject bulletInstantiationPoint;
    public float guardHealth;


    // Start is called before the first frame update
    void Start()
    {
        guardHealth = 100f;
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        guardsInDistance = new List<GameObject>();

        pointCount = Random.Range(0, patrolPoints.Length - 1);
        alerted = false;
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (agent.remainingDistance < agent.stoppingDistance || (agent.path.status == NavMeshPathStatus.PathPartial))
        {
            int oldPointCount = pointCount;
            pointCount = Random.Range(0, patrolPoints.Length);
            
            while(pointCount == oldPointCount)
            {
                pointCount = Random.Range(0, patrolPoints.Length);
            }
        }

        if (!alerted)
        {
            agent.SetDestination(patrolPoints[pointCount].transform.position);
        }
        


    }

    public IEnumerator GoThroughTheAlertZone(Vector3 destination, bool hasSeenPlayer)
    {
        agent.isStopped = false;
        animator.SetBool("Fire", false);
        if(hasSeenPlayer)
            foreach (GameObject guard in guardsInDistance)
            {
                IEnumerator coroutine = guard.GetComponent<EnemyController>().GoThroughTheAlertZone(destination, false);
                StartCoroutine(coroutine);
            }
        agent.SetDestination(destination);
        yield return new WaitUntil(() => agent.remainingDistance < agent.stoppingDistance);
        List<Transform> patrolPointsInDistance = FindPatrolPointsInRadius(30);
        foreach (Transform point in patrolPointsInDistance)
        {
            agent.SetDestination(point.position);
            yield return new WaitUntil(() => agent.remainingDistance == 0);
        }
        alerted = false;

    }
    
    public void ShootPlayer(Transform playerTransform)
    {
        agent.isStopped = true;
        alerted = true;
        animator.SetBool("ShouldMove", false);
        transform.LookAt(playerTransform);
        animator.SetBool("Fire", true);
        
    }



    public void AlertObj(RaycastHit raycast)
    {
        alerted = true;
        GoThroughTheAlertZone(raycast.transform.position, true);
        for (int j = 0; j < guardsInDistance.Count; j++)
        {
            guardsInDistance[j].GetComponent<EnemyController>().GoThroughTheAlertZone(raycast.transform.position, false);
        }
    }

    public List<Transform> FindPatrolPointsInRadius(float radius)
    {
        List<Transform> returnArr = new List<Transform>();
        foreach(GameObject patrolPoint in patrolPoints)
        {
            float distanceMagn = (transform.position - patrolPoint.transform.position).sqrMagnitude;
            if(distanceMagn < radius*radius)
            {
                returnArr.Add(patrolPoint.transform);
            }
        }

        return returnArr;
    }

    public void FireBullet()
    {
        GameObject clone = Instantiate(bullet, bulletInstantiationPoint.transform.position, Quaternion.identity);
        clone.GetComponent<Bullet>().direction = transform.forward;
        Destroy(clone, 2f);

    }


    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Guard"))
            //guardsInDistance.Add(other.gameObject);
        if (other.gameObject.CompareTag("Bullet"))
        {
            guardHealth -= 40f;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Guard"))
            //guardsInDistance.Remove(other.gameObject);
    }

}
