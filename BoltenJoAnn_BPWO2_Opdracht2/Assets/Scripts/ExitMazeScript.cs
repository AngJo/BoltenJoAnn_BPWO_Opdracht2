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
            GameManager.Instance.timer = 1f;
        }
        
        //GameManager.Instance.leftStartBlock = true;
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(transform.parent.parent.gameObject.name);
        if(transform.parent.parent.gameObject == GameManager.Instance.previousRoom) {
            Destroy(GameManager.Instance.previousRoom);
            GameManager.Instance.ClearFaeArray();
        }
    }


}
