using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    private static DialogueScript instance;
    public static DialogueScript Instance
    {
        get { return instance; }
    }

    public GameObject textBox;
    public bool isCoroutineStarted = false;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("DialogueManager can only have one instance");
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartingDialogue());
        //if (this.tag == "Fairy")
        //{
        //    StartCoroutine(FairyDialogue());
        //}
    }
    private void Update()
    {
        
    }

    IEnumerator StartingDialogue()
    {
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "I need to hurry home...";
        yield return new WaitForSeconds(4);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
    }

    public IEnumerator ToolTipText()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "Press E to talk";
        isCoroutineStarted = false;
    }

    public IEnumerator noText()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(0.5f);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isCoroutineStarted = false;
    }

    public IEnumerator FairyDialogue()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "I'm a fairy and I may or may not tell lies.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "I bet you can't tell who of us is lying.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isCoroutineStarted = false;
    }

    public IEnumerator NeutralDialogue()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "This is the neutral Dialogue";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isCoroutineStarted = false;
    }

    public IEnumerator TruthDialogue()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "I speak the truth.";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isCoroutineStarted = false;
    }

    public IEnumerator Liar1Dialogue()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "I am Liar #1";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isCoroutineStarted = false;
    }

    public IEnumerator Liar2Dialogue()
    {
        isCoroutineStarted = true;
        yield return new WaitForSeconds(1);
        textBox.GetComponent<TextMeshProUGUI>().text = "I am Liar #2";
        yield return new WaitForSeconds(3);
        textBox.GetComponent<TextMeshProUGUI>().text = "";
        isCoroutineStarted = false;
    }


}
