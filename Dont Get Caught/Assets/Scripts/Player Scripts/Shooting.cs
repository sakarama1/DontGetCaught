using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingPoint;

    void ShootEvent()
    {
        GameObject clone = Instantiate(bullet, shootingPoint.transform.position, Quaternion.identity);
        clone.GetComponent<Bullet>().direction = transform.forward;
        Destroy(clone, 2f);
    }
}
