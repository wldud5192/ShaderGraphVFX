using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    SphereProperties sphereManager;
    public float currentTime;
    float startingTime = 30f;
    Text countdownText;

    public int level;
    public Text levelText;

    public int clientState = 4;
    public Text clientStateText;

    bool loadFinished = false;
    bool allMatch = false;

    // Start is called before the first frame update
    void Awake()
    {
        sphereManager = GameObject.Find("SphereManager").GetComponent<SphereProperties>();
        countdownText = GetComponent<Text>();
        level = 1;
        resetTimer();
        levelText.text = level.ToString("0");
    }

    void resetTimer()
    {
        currentTime = startingTime;
        countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow10", false);
        countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow5", false);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
                
        if (currentTime < 11)
        {
            countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow10", true);
        }
        if (currentTime < 5)
        {
            countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow5", true);
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            if (!loadFinished)
            {
                StartCoroutine(TimeOver());
            }

        }

        foreach (bool matching in sphereManager.matchProp)
        {
            if (matching)
            {
                allMatch = true;
            } else
            {
                allMatch = false;
                break;
            }

            if (allMatch)
            {
                if (!loadFinished)
                {
                    StartCoroutine(Success());
                }
            }
        }
    }


    IEnumerator TimeOver()
    {
        loadFinished = true;
        Debug.Log("Time over!");
        clientState--;
        if(clientState == 3)
        {
            //Annoyed
            clientStateText.text = "Annoyed";
        }
        if (clientState == 2)
        {
            //Angry
            clientStateText.text = "Angry";
        }
        if (clientState == 1)
        {
            //About to explode...
            clientStateText.text = "...";
        } if (clientState == 0)
        {
            Debug.Log("Game Over!");
        }


        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        resetTimer();
        loadFinished = false;
        yield break;
    }

    IEnumerator Success()
    {
        loadFinished = true;
        Debug.Log("Success!");
        level++;
        levelText.text = level.ToString("0");

        startingTime--;

        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        resetTimer();
        loadFinished = false;
        yield break;

    }

}
