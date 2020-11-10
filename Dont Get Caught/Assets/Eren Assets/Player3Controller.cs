using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player3Controller : MonoBehaviour
{
    public Joystick joystick;
    public GameObject child;
    public GameObject manager;
    public GameObject hand;
    public GameObject weapons;

    GameManager gamemanager;
    SkinManager skinManager;
    WeaponsManager weaponsManager;
    UIManager uIManager;

    public GameObject HealthBar;
    Slider slider;
    public Gradient healthGradient;
    public Image fill;

    private Rigidbody rb;
    private Vector3 moveDir;
    public Animator animator;

    float mHorizontal = 0f;
    float mVertical = 0f;

    public float speed;

    public bool isDead;
    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = manager.GetComponent<GameManager>();
        skinManager = manager.GetComponent<SkinManager>();
        weaponsManager = manager.GetComponent<WeaponsManager>();
        uIManager = manager.GetComponent<UIManager>();

        child = transform.GetChild(skinManager.selectedNumber).gameObject;
        child.SetActive(true);

        //hand = child.GetComponent<FindHand>().Hand;

        //weapons.transform.SetParent(hand.transform);
        //weapons.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        //weapons.transform.GetChild(weaponsManager.selectedNumber).gameObject.SetActive(true);

        //speed = 5;
        rb = GetComponent<Rigidbody>();
        animator = child.GetComponent<Animator>();

        slider = HealthBar.GetComponent<Slider>();

        uIManager.collectedMoneyText.text = "+ " + gamemanager.collectedMoney;
        uIManager.PtotalMoneyText.text = "+ " + gamemanager.collectedMoney;

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

        animator.SetFloat("Running", moveDir.magnitude);

        if(slider.value <= 0)
        {
            die();
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
            animator.SetTrigger("Attack");
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

            uIManager.collectedMoneyText.text = "+ " + gamemanager.collectedMoney;
            uIManager.PtotalMoneyText.text = "+ " + gamemanager.collectedMoney;
        }

        if (other.CompareTag("Damage"))
        {
            //make more general
            getDamaged(10);
        }

        if (other.CompareTag("Finish"))
        {
            finished = true;
            animator.SetTrigger("Finish"); //rumba dance

            //ui stuff
            gamemanager.EndGame();
            uIManager.inGameUI.SetActive(false);
            uIManager.nextLevelUI.SetActive(true);

            //stop the time
            Time.timeScale = 0;
        }

        if (other.CompareTag("Vault"))
        {
            gamemanager.finish.SetActive(true);
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

    public void getDamaged(int damage)
    {
        slider.value -= damage;
    }

    public void die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void setZero()
    {
        if (isDead)
        {
            rb.velocity = Vector3.zero;
        }

        if (finished)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
