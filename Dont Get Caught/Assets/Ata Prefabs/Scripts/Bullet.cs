using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;
    public Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().health -= Random.Range(7, 15);
        }
        Destroy(gameObject);
    }
}
