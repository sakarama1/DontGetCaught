using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plyer1Script : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        float moveVertical = Input.GetAxis("Vertical");
        anim.SetFloat("Speed", moveVertical);

        //float moveHorizontal = Input.GetAxis("Horizontal");
    }
}
