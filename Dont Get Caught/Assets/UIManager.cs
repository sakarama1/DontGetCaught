using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    [Header("Canvas")]
    public GameObject startMenuUI;
    public GameObject inGameUI;
    public GameObject pauseUI;
    public GameObject settingsUI;
    public GameObject shopUI;
    public GameObject weaponsUI;
    public GameObject nextLevelUI;
    public GameObject gameOverUI;

    [Header("Start Menu UI")]
    public Text levelText;
    public Text totalMoneyText;

    [Header("In Game UI")]
    public Text IGlevelText;
    public Text collectedMoneyText;

    [Header("Pause UI")]
    public Color onColor;
    public Color offColor;

    public GameObject PsoundButton;
    public GameObject PbloodButton;

    public Text PlevelText;
    public Text PtotalMoneyText;

    [Header("Settings UI")]
    public GameObject SsoundButton;
    public GameObject SbloodButton;

    [Header("Shop UI")]
    public GameObject[] shopUnlocks;

    [Header("Weapons UI")]
    public GameObject[] weaponsUnlocks;

    [Header("Next Level UI")]
    public Text nextMoneyCollectedText;
    public Color okeyColor;
    public Image[] levelForReward;

    [Header("Game Over UI")]
    public float waitFactor;
    public bool startCountDown;

    public Text moneyCollectedText;
    public Text noThanksText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Start Menu Functions

    public void startNoAds()
    {
        //inapppurchase
    }

    public void startGoToSettings()
    {
        startMenuUI.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void startGoToShop()
    {
        startMenuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void startGoToWeapons()
    {
        startMenuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void startStartText()
    {
        startMenuUI.SetActive(false);
        shopUI.SetActive(true);
    }

    //In Game Functions
    public void InGamePause()
    {
        Time.timeScale = 0;
        inGameUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    //Pause Functions
    public void PauseBackToMenu()
    {
        pauseUI.SetActive(false);
        startMenuUI.SetActive(true);
        gameManager.RestartGame();
    }

    public void PauseBackToGame()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        inGameUI.SetActive(true);
    }

    //Settings Functions
    public void setiingsBackToMenu()
    {
        settingsUI.SetActive(false);
        startMenuUI.SetActive(true);
    }

    //Shop Functions
    public void ShopBackToMenu()
    {
        shopUI.SetActive(false);
        startMenuUI.SetActive(true);
    }

    public void ShopUnlock()
    {

    }

    public void ShopGetMoney()
    {

    }

    //Weapons Functions
    public void WeaponsBackToMenu()
    {
        weaponsUI.SetActive(false);
        startMenuUI.SetActive(true);
    }

    public void WeaponsUnlock()
    {

    }

    public void WeaponsGetMoney()
    {

    }

    //Next Level Functions
    public void nextLevelGoNext()
    {
        Time.timeScale = 1;
        gameManager.RestartGame();
        PlayerPrefs.SetInt("totalSouls", gameManager.totalMoney);
    }

    public void nextLevelDoubleReward()
    {
        //Advertisements.Instance.ShowRewardedVideo(rewardAndGo);
    }

    //private void rewardAndGo(bool arg0)
    //{
    //    gameManager.totalSouls += sorcererController.soulsCollected;
    //    goToNextLevel();
    //}

    //Game Over Functions
    public void gameOverReplay()
    {
        gameManager.EndGame();
        gameManager.RestartGame();
    }

    public void gameOverRevive()
    {
        //Advertisements.Instance.ShowRewardedVideo(makeRevive);
    }

    //private void makeRevive(bool arg0)
    //{
    //    gameManager.Revive();
    //}

    //Shop Functions

    //Weapons Functions

    //Global Functions
    public void toggleSound()
    {
        if (gameManager.soundOn)
        {
            gameManager.soundOn = false;
            PsoundButton.GetComponent<Image>().color = offColor;
            SsoundButton.GetComponent<Image>().color = offColor;
        }

        else
        {
            gameManager.soundOn = true;
            PsoundButton.GetComponent<Image>().color = onColor;
            SsoundButton.GetComponent<Image>().color = onColor;
        }
    }

    public void toggleBlood()
    {
        if (gameManager.bloodOn)
        {
            gameManager.bloodOn = false;
            PbloodButton.GetComponent<Image>().color = offColor;
            SbloodButton.GetComponent<Image>().color = offColor;
        }

        else
        {
            gameManager.bloodOn = true;
            PbloodButton.GetComponent<Image>().color = onColor;
            SbloodButton.GetComponent<Image>().color = onColor;
        }
    }
}
