using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Controller : MonoBehaviour
{
    public Joystick joystick;

    private Rigidbody rb;
    private Vector3 moveDir;
    Animator anim;

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

        SetMoveDir();

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

        moveDir = new Vector3(mHorizontal, 0, mVertical).normalized;

        SetMoveDir();

        anim.SetFloat("Running", moveDir.magnitude);
    }

    //input.getaxislerle degistirilecek
    void JoystickInput()
    {
        mHorizontal = joystick.Horizontal;
        mVertical = joystick.Vertical;
    }

    //set move direction
    void SetMoveDir()
    {
        if (Mathf.Abs(mHorizontal) >= .5f || Mathf.Abs(mVertical) >= .5f)
        {
            moveDir = new Vector3(mHorizontal, 0, mVertical).normalized;
        }
        else
        {
            moveDir = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            anim.SetTrigger("Attack");
        }
    }
}
