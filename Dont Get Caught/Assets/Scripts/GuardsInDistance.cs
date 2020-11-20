using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardsInDistance : MonoBehaviour
{
    EnemyController enemyController;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guard"))
            enemyController.guardsInDistance.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Guard"))
            enemyController.guardsInDistance.Remove(other.gameObject);
    }
}
