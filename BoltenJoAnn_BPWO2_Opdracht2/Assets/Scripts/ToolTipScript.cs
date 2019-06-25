using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipScript : MonoBehaviour
{
    //public GameObject textbox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //textbox.GetComponent<TextMeshProUGUI>().text = "Press E to talk";
            if (DialogueScript.Instance.isCoroutineStarted == false) { StartCoroutine(DialogueScript.Instance.ToolTipText()); }
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        //textbox.GetComponent<TextMeshProUGUI>().text = "";
        StartCoroutine(DialogueScript.Instance.noText());
    }
}
