using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Bug: When you run into a mushroom and press SpaceBar, you will no longer be able to jump


public class GameManager : MonoBehaviour
{

    // Derived from the Diggers game created in class



    public static GameManager Instance {get; private set;}
    public GameObject mainScreen;
    public GameObject creditsScreen;
    public GameObject howToPlayScreen;
    public GameObject ratDeadScreen;
    public GameObject endScreen;
    public GameObject curtain;
    private bool raiseLower = false;
    public GameObject canvas;
    public GameObject eventSystem;


    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    public void DialogShow(string text) {
        dialogBox.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(TypeText(text));
    }

    public void DialogHide() {
        dialogBox.SetActive(false);
    }

    IEnumerator TypeText(string text) {
        dialogText.text = "";
        foreach (char c in text.ToCharArray()) {
            dialogText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
    }

    void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(canvas);
                DontDestroyOnLoad(eventSystem);
            } else {
                Destroy(gameObject);
                Destroy(canvas);
                Destroy(eventSystem);
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

    public void EndScreen() {
        // StartCoroutine(LoadYourAsyncScene("EndScreen"));
        StartCoroutine(LoadYourAsyncScene("StartScreen"));
        endScreen.SetActive(true);
    }

    public void RatDead() {
        // StartCoroutine(LoadYourAsyncScene("RatDead"));
        StartCoroutine(LoadYourAsyncScene("StartScreen"));
        ratDeadScreen.SetActive(true);
    }

    public void Tutorial() {
        // StartCoroutine(LoadYourAsyncScene("HowToPlay"));
        mainScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void ShowCredits() {
        // StartCoroutine(LoadYourAsyncScene("Credits"));
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void StartGame() {
        StartCoroutine(LoadYourAsyncScene("MainGame"));
        mainScreen.SetActive(false);
    }

    public void ReturnToStartScreen() {
        creditsScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
        ratDeadScreen.SetActive(false);
        endScreen.SetActive(false);
        // StartCoroutine(LoadYourAsyncScene("StartScreen"));
        mainScreen.SetActive(true);
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
