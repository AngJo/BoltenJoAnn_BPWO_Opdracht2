using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject fairy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.leftStartBlock == true && GameManager.Instance.newLevelBlockMade == true)
        //{
        //if (GameManager.Instance.newLevelBlockMade == true)
        //{
            //GameManager.Instance.CheckPlayerDistanceToPreviousBlock();
        //}
            
        //}


        

        if (Input.GetKeyUp(KeyCode.E))
        {

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Fairy"))
                {
                    fairy = hit.collider.gameObject;
                    FairyScript faeDialogue = fairy.GetComponent<FairyScript>();
                    faeDialogue.PlayDialogue();
                    

                    //Start Dialogue script from fairy
                    //fairy.playdialogue
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            GameManager.Instance.AssigNewLiar();
        }
    }
}
