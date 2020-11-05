using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;

    const float locomotionAnimationSmoothTime = .1f;
    public float speed;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        speed = 0;
        lastPosition = Vector3.zero;
    }

    private void Update()
    {
        float speedPercent = speed / 3.8f;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //Calculating speed by hand
        speed = (transform.position - lastPosition).magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;
        //Debug.Log("Speed: " + speed); maxi 3.8 gibi
    }

}
