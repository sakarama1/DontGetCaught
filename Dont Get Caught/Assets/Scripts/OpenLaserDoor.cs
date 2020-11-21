using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLaserDoor : MonoBehaviour
{

    public GameObject laserDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openIt()
    {
        laserDoor.SetActive(false);
        transform.GetChild(0).transform.localPosition = new Vector3(0, -0.07f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            openIt();
        }
    }
}
