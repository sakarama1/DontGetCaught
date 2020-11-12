using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevel;

    public int totalMoney;
    public int collectedMoney;

    public bool bloodOn;
    public bool soundOn;

    public bool noAds;

    public int gameCount;
    public int gameCountBeforeAdd;

    public GameObject player;

    public GameObject[] levels;

    public GameObject finish;
    public GameObject vault;

    public GameObject level;

    UIManager uiManager;

    private void Awake()
    {
        //Instantiate(levels[currnetLevel]);
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = gameObject.GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player");

        //if (audioOn) gameObject.GetComponent<AudioSource>().enabled = true;
        //else gameObject.GetComponent<AudioSource>().enabled = false;

        //Adds
        //Advertisements.Instance.Initialize();

        ///////////////////Load Data/////////////////////////////
        //Initialize the current level and spawn the enemies

        ////////////////////Testing//////////////////////////
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("CurrentLevel", 1);
        //PlayerPrefs.SetInt("totalSouls", 50);

        //currentLevel = PlayerPrefs.GetInt("currentLevel");
        //totalMoney = PlayerPrefs.GetInt("totalMoney");

        //audioOn = skillManager.intToBool(PlayerPrefs.GetInt("audioOn"));
        //soundOn = skillManager.intToBool(PlayerPrefs.GetInt("soundOn"));

        //gameCount = PlayerPrefs.GetInt("gameCount");

        //whether to show add or not
        //noAds = skillManager.intToBool(PlayerPrefs.GetInt("Adds"));

        //Set level UI
        uiManager.levelText.text = "Level " + currentLevel;
        uiManager.IGlevelText.text = "Level " + currentLevel;
        uiManager.PlevelText.text = "Level " + currentLevel;

        uiManager.totalMoneyText.text = totalMoney + "";

        level = Instantiate(levels[currentLevel]);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player3Controller>().isDead)
        {
            player.GetComponent<Player3Controller>().enabled = false;

            uiManager.gameOverUI.SetActive(true);
            uiManager.inGameUI.SetActive(false);

            //play sound
            //gameObject.GetComponent<AudioSource>().clip = deathClip;
            //gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void EndGame()
    {
        //arrange player
        player.GetComponent<Player3Controller>().enabled = false;
        player.GetComponent<Player3Controller>().setZero();

        uiManager.inGameUI.SetActive(false);
        uiManager.nextLevelUI.SetActive(true);

        //save total coins
        totalMoney += collectedMoney;
        PlayerPrefs.SetInt("totalMoney", totalMoney);

        //save current level
        ++currentLevel;
        PlayerPrefs.SetInt("currentLevel", currentLevel);

        //Count the games
        ++gameCount;
        PlayerPrefs.SetInt("gameCount", gameCount);

        //display adds 
        if (gameCount >= gameCountBeforeAdd && !noAds)
        {
            gameCount = 0;
            PlayerPrefs.SetInt("gameCount", gameCount);
            //Advertisements.Instance.ShowInterstitial();
        }

        Destroy(level);
    }

    public void Revive()
    {
        player.GetComponent<Player3Controller>().enabled = true;
        player.GetComponent<Player3Controller>().isDead = false;
        player.GetComponent<Player3Controller>().animator.SetTrigger("Revive");

        //change UI
        uiManager.gameOverUI.SetActive(false);
        uiManager.inGameUI.SetActive(true);

        ////reactivate enemies
        ///
        //foreach (GameObject enemy in enemies)
        //{
        //    enemy.GetComponent<EnemyController>().setStart();
        //}
    }

    public void RestartGame()
    {
        //load the scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        //arrange player
        player.GetComponent<Player3Controller>().enabled = true;
        player.GetComponent<Player3Controller>().finished = false;

        //manage UI
        uiManager.startMenuUI.SetActive(false);
        uiManager.inGameUI.SetActive(true);

        player.GetComponent<Player3Controller>().Canvas.SetActive(true);
        player.transform.rotation = Quaternion.identity;
    }
}
