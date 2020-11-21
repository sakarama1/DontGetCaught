using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    private EnemyController enemyController;
    private FieldOfView fieldOfView;
    private IEnumerator coroutine;
    private bool startedPlaying;
    private ParticleSystem shockedParticleSystem;

    public GameObject[] emojis;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        fieldOfView = transform.GetChild(0).GetComponent<FieldOfView>();

        startedPlaying = false;
        shockedParticleSystem = emojis[0].GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.detectedPlayer && !startedPlaying)
        {
            startedPlaying = true;
            coroutine = emojiStartPlaying();
            StartCoroutine(coroutine);
        }
        else if(!fieldOfView.detectedPlayer && enemyController.alerted && !startedPlaying)
        {
            emojis[0].SetActive(false);
            emojis[1].SetActive(false);
            emojis[2].SetActive(true);
        }
        else if(!fieldOfView.detectedPlayer && !enemyController.alerted)
        {
            foreach(GameObject emoji in emojis)
            {
                emoji.SetActive(false);
            }
        }
    }

    private IEnumerator emojiStartPlaying()
    {
        Debug.Log("Started");
        foreach (GameObject emoji in emojis)
        {
            emoji.SetActive(false);
        }
        emojis[0].SetActive(true);
        yield return new WaitForSeconds(shockedParticleSystem.main.duration);
        emojis[0].SetActive(false);
        emojis[1].SetActive(true);
        yield return new WaitUntil(() => !fieldOfView.detectedPlayer);
        startedPlaying = false;
    }
}
