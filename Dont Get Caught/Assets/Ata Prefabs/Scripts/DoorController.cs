using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator animator;    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guard") && !other.isTrigger)
        {
            animator.SetBool("OpenDoor", true);
        }  
    }
    public void OnAnimationExit()
    {
        Debug.Log("Called");
        animator.SetBool("OpenDoor", false);
    }


}
