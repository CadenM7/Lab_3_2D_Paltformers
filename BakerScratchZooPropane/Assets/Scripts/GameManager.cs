using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Bug: When you run into a mushroom and press SpaceBar, you will no longer be able to jump


public class GameManager : MonoBehaviour
{

    // Inspired from the Diggers game created in class

    public static GameManager Instance {get; private set;}
    public GameObject mainScreen;
    public GameObject curtain;
    private bool raiseLower = false;
    public GameObject canvas;
    public GameObject eventSystem;


    void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(canvas);
                DontDestroyOnLoad(eventSystem);
            } else {
                Destroy(gameObject);
            }

        }

    IEnumerator ColorLerpFunction(bool fadeout, float duration)
    {
        float time = 0;
        raiseLower = true;
        Image curtainImg = curtain.GetComponent<Image>();
        Color startValue;
        Color endValue;
        if (fadeout) {
            startValue = new Color(0, 0, 0, 0);
            endValue = new Color(0, 0, 0, 1);
        } else {
            startValue = new Color(0, 0, 0, 1);
            endValue = new Color(0, 0, 0, 0);
        }

        while (time < duration)
        {
            curtainImg.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        curtainImg.color = endValue;
        raiseLower = false;
    }

     IEnumerator LoadYourAsyncScene(string scene)
     {
        StartCoroutine(ColorLerpFunction(true, 1));

        while (raiseLower)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        StartCoroutine(ColorLerpFunction(false, 1));
    }

    public void Tutorial() {
        StartCoroutine(LoadYourAsyncScene("HowToPlay"));
        mainScreen.SetActive(false);
    }

    public void EndGame() {
        StartCoroutine(LoadYourAsyncScene("EndScreen"));
        mainScreen.SetActive(false);
    }

    public void ShowCredits() {
        StartCoroutine(LoadYourAsyncScene("Credits"));
        mainScreen.SetActive(false);
    }

    public void StartGame() {
        StartCoroutine(LoadYourAsyncScene("MainGame"));
        mainScreen.SetActive(false);
    }

    public void ReturnToStartScreen() {
        StartCoroutine(LoadYourAsyncScene("StartScreen"));
        mainScreen.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
