using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    
    public string text;

    public void OnCollisionEnter2D(Collision2D collision2D) {
        print("Entered..");
        if (collision2D.gameObject.CompareTag("Player")) {
            DialogManager.Instance.DialogShow(text);
        }
    }
    public void OnCollisionExit2D(Collision2D collision2D) {
        if (collision2D.gameObject.CompareTag("Player")) {
            DialogManager.Instance.DialogHide();
        }
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
