using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyScript : MonoBehaviour
{
 
    public LiarState state;
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = this.GetComponent<Animator>();
        //anim.Play("idle3");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayDialogue()
    {
        DialogueScript.Instance.activeAnimator = GetComponent<Animator>();
        print(name);
        switch (state)
        {
            case LiarState.Neutral:
                //play dialogue
                if (DialogueScript.Instance.isCoroutineStarted == false)
                {
                    StartCoroutine(DialogueScript.Instance.NeutralDialogue());
                }
                break;
            case LiarState.Honest:
                if (DialogueScript.Instance.isCoroutineStarted == false)
                {
                    StartCoroutine(DialogueScript.Instance.TruthDialogue());
                }
                break;
            case LiarState.Liar1:
                if (DialogueScript.Instance.isCoroutineStarted == false)
                {
                    StartCoroutine(DialogueScript.Instance.Liar1Dialogue());
                }
                break;
            case LiarState.Liar2:
                if (DialogueScript.Instance.isCoroutineStarted == false)
                {
                    StartCoroutine(DialogueScript.Instance.Liar2Dialogue());
                }
                break;
            default:
                break;
        }
    }


    public enum LiarState
    {
        Neutral,
        Honest,
        Liar1,
        Liar2
    }
}
