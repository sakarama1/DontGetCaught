using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3Controller : MonoBehaviour
{
    public Joystick joystick;
    public GameObject child;
    public GameObject manager;
    GameManager gamemanager;

    public GameObject HealthBar;
    Slider slider;
    public Gradient healthGradient;
    public Image fill;

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

        gamemanager = manager.GetComponent<GameManager>();

        slider = HealthBar.GetComponent<Slider>();

        InvokeRepeating("AdjustPlayerPos", 0f, 2f);
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

        if(slider.value <= 0)
        {
            //player die animation
            //game ends
        }
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
        if (other.CompareTag("Guard"))
        {
            anim.SetTrigger("Attack");
        }

        if (other.CompareTag("Ammo"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Health"))
        {
            Destroy(other.gameObject);
            slider.value += 30;
            fill.color = healthGradient.Evaluate(slider.normalizedValue); //health bar changes color accordingly
        }

        if (other.CompareTag("Money"))
        {
            Destroy(other.gameObject);
            gamemanager.collectedMoney += 5;
            
        }
    }

    //method for setting player's max health from other scripts
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health; //slider starts at maximum health

        fill.color = healthGradient.Evaluate(1f); //slider gradient color is initially green
    }

    void AdjustPlayerPos()
    {
        child.transform.position = transform.position;
    }
}
