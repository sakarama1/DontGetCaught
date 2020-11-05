using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{

    //number of selected skin
    public int selectedNumber;

    //number of selected skin
    public int pickedNumber;

    //use them to calculate the total cost
    public int[] costs;

    public int numberofUnlocked;

    //bool array to control if any element is unlocked
    public bool[] unlocked;

    //colors to show in scrollable panel
    public Color notSelectedColor;
    public Color selectedColor;

    UIManager uiManager;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //initialize
        uiManager = gameObject.GetComponent<UIManager>();
        gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //used to select the skills in skills canvas
    //selectedSkill is the skill you just clicked
    public void selectSkill(int index)
    {
        pickedNumber = index;

        uiManager.weaponsCostText.text = ": " + costs[index];

        //after you decided if it is unlocked or not
        //decide if you can select this skill to use in the game
        if (unlocked[index])
        {
            uiManager.selectButton.SetActive(true);
            uiManager.unlockButton.SetActive(false);
        }

        else
        {
            uiManager.unlockButton.SetActive(true);
            uiManager.selectButton.SetActive(false);
        }
    }
}
