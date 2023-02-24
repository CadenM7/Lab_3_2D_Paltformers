using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Rigidbody2D body;
    public float horizontal;
    public GameObject player;
    public static GameManager Instance {get; private set;}

    public bool gameOver = false;
    public AudioSource audioSource;
    public GameObject mainScreen;
    public GameObject curtain;
    private bool raiseLower = false;
    public GameObject canvas;
    public GameObject eventSystem;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool jumping = false;

    public float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("horizontal", horizontal);
        if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
            print("Space!");
            body.AddForce(new Vector2(0, 400));
            jumping = true;
    }
        if (transform.position.y <= -5) {
            print("falling");
            StartCoroutine("RatFall");
            GameManager.Instance.RatDead();
            player.transform.position = new Vector2(player.transform.position.x, 15000f);
        }

        if (!gameOver) {
            if (transform.position.x >= 675) {
                GameManager.Instance.EndScreen();
                gameOver = !gameOver;
            }
        }
    }

    IEnumerator RatFall() {
        audioSource.Play();
        return null;
    }

    void FixedUpdate() {
        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
        }

    void OnCollisionEnter2D(Collision2D collision2D) {
            jumping = false;
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

    public void ReturnToStartScreen() {
        StartCoroutine(LoadYourAsyncScene("RatDead"));
        mainScreen.SetActive(false);
    }

    public void FinishGame() {
        StartCoroutine(LoadYourAsyncScene("EndScreen"));
        mainScreen.SetActive(false);
    }
}
