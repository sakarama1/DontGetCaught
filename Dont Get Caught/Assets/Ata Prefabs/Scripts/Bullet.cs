using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed;
    Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 100f;
        direction = transform.root.forward;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
