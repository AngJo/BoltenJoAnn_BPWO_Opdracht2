using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMazeScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.timer <= 0f)
        {
            print("player has entered a trigger");
            GameManager.Instance.CreateNewLevelBlock(other.gameObject, this.tag);
        }
        else
        {
            GameManager.Instance.timer = 1f;
        }
        
        
        //GameManager.Instance.leftStartBlock = true;
    }

    
}
