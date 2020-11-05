using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sik : MonoBehaviour
{
    public Joystick joystick;

    private Rigidbody rb;
    private Vector3 moveDir;
    Animator anim;
    const float locomotionAnimationSmoothTime = .1f;

    float mHorizontal = 0f;
    float mVertical = 0f;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //speed = 5;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //mHorizontal = Input.GetAxis("Horizontal");
        //mVertical = Input.GetAxis("Vertical");

        JoystickInput();

        rb.angularVelocity = Vector3.zero;

        if (mHorizontal >= .3f || mVertical >= .3f)
        {
            moveDir = new Vector3(mHorizontal, 0, mVertical).normalized;
        }
        else
        {
            moveDir = Vector3.zero;
        }

        rb.velocity = moveDir * speed;

        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 5 * Time.deltaTime); //player faces where it moves
        }
    }

    private void Update()
    {
        //mHorizontal = Input.GetAxis("Horizontal");
        //mVertical = Input.GetAxis("Vertical");

        JoystickInput();

        if (mHorizontal != 0)
        {
            anim.SetFloat("Running", Mathf.Abs(mHorizontal));
        }
        if (mVertical != 0)
        {
            anim.SetFloat("Running", Mathf.Abs(mVertical));
        }
        else
        {
            anim.SetFloat("Running", 0f);
        }

        //anim.SetFloat("Running", Mathf.Abs(mHorizontal));
        //anim.SetFloat("Running", Mathf.Abs(mVertical));
    }

    //input.getaxislerle degistirilecek
    void JoystickInput()
    {
        mHorizontal = joystick.Horizontal;
        mVertical = joystick.Vertical;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            anim.SetTrigger("Attack");
        }
    }
}
