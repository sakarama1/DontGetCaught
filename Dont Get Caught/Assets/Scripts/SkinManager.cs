using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    //number of selected skin
    public int selectedNumber; 

    //use them to calculate the total cost
    public int cost;
    public int costMult;
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
        //after you decided if it is unlocked or not
        //decide if you can select this skill to use in the game
        if (unlocked[index])
        {
            uiManager.shopUnlocks[selectedNumber].GetComponent<Image>().color = notSelectedColor;

            selectedNumber = index;

            //save it in database
            PlayerPrefs.SetInt("selectedNumberSkills", index);

            //change the color of the panel to give the user a feedback
            uiManager.shopUnlocks[index].GetComponent<Image>().color = selectedColor;            
            return;
        }
    }
}
