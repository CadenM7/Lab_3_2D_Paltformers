using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D body;
    public float horizontal;

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
    }

    void FixedUpdate() {
        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
        }

    void OnCollisionEnter2D(Collision2D collision2D) {
            jumping = false;
        }
    }
