using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public static DialogManager Instance {get; private set;}

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
