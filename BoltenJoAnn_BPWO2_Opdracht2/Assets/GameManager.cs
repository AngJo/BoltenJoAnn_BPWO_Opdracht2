using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public GameObject levelBlock;

    public GameObject currentRoom;
    public GameObject previousRoom;
    public GameObject storyItem1;
    public GameObject storyItem2;
    public GameObject storyItem3;

    public bool newLevelBlockMade = false;
    public bool leftStartBlock = false;
    public bool checkedTrigger = false;
    [SerializeField]
    private int activatedTrigger;
    private string enteredTrigger;
    [SerializeField]
    private int correctPath = 0;
    private GameObject wall;

    [SerializeField]
    private int wrongChoice = 0;
    [SerializeField]
    private int rightChoice = 0;

    // 0 = n, 1 = w, 2 = s, 3 = e
    public int truth = 0;
    public int neutral = 0;
    public List<FairyScript> fae = new List<FairyScript>();


    public Transform playerPos;
    public GameObject northWall;

    public string trigger;
    public float timer = 0f;

    private void Awake()
    {
       if (instance != null)
        {
            Debug.LogError("GameManager can only have one instance");
        }
        instance = this;
        DontDestroyOnLoad(this);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        fae = FindObjectsOfType<FairyScript>().ToList();
        AssigNewLiar();
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        if (wrongChoice >= 3)
        {
            LoadLoseCase();
        }
        else if (rightChoice >= 4)
        {
            LoadWinCase();
        }

    }

    void SpawnStoryObject()
    {
        GameObject storyItem;
        switch (rightChoice)
        {
            case 1: storyItem = Instantiate(storyItem1, new Vector3(0, 0, 0), Quaternion.identity, currentRoom.transform); storyItem.transform.localPosition = new Vector3(0, 0.15f, 0);  break;
            case 2: storyItem = Instantiate(storyItem2, new Vector3(0, 0, 0), Quaternion.identity, currentRoom.transform); storyItem.transform.localPosition = Vector3.zero; break;
            case 3: storyItem = Instantiate(storyItem3, new Vector3(0, 0, 0), Quaternion.identity, currentRoom.transform); storyItem.transform.localPosition = Vector3.zero; break;
        }
    }

    void ShuffleArray()
    {
        for (int i = 0; i < fae.Count; i++)
        {
            FairyScript fairy = fae[i];
            //Get new position to move i to
            int rndPos = UnityEngine.Random.Range(0, i);
            // Current fairy position = new fairy position
            fae[i] = fae[rndPos];
            //Input information into randomPos
            fae[rndPos] = fairy;

        }
    }

    public void AssigNewLiar()
    {
        //fae = new List<FairyScript>(4);
        

        truth = UnityEngine.Random.Range(0, fae.Count);
        correctPath = truth;
        neutral = UnityEngine.Random.Range(0, fae.Count);
        if (neutral == truth)
        {
            neutral = UnityEngine.Random.Range(0, fae.Count);
        }
        else
        {
            for (int i = 0; i < fae.Count; i++)
            {

                if (i == truth)
                {
                    fae[i].state = FairyScript.LiarState.Honest;
                }
                else if (i == neutral)
                {
                    fae[i].state = FairyScript.LiarState.Neutral;
                }
                else
                {
                    fae[i].state = FairyScript.LiarState.Liar1;
                }
            }
        }
        
    }
    
    void ChangeName()
    {
        if (fae == null)
        {
            fae = FindObjectsOfType<FairyScript>().ToList<FairyScript>();
        }
        print("Checking name");

        //Als er nog geen previousLevel is
        //Vind object met naam New Level (Current Level block)
        previousRoom = currentRoom;
        //Rename current Level block to previous
        previousRoom.name = "previousLevel";
        print("Room name changed from newLevel to previousLevel");

        print(trigger);
            
        CreateLevelBlock(trigger);
        //CHECK distance to player from wall
    }

    public void CreateNewLevelBlock(GameObject player, string triggerWall)
    {
        
        trigger = triggerWall;
        CheckTrigger(trigger);
        //checkedTrigger = true;
        ChangeName();
        

        if (activatedTrigger == correctPath)
        {
            
            rightChoice += 1;
            AssigNewLiar();
            SpawnStoryObject();
        }
        else { wrongChoice += 1; }
    }

    void CreateLevelBlock(string trigger)
    {
        print("Creating new level");
        print(trigger);
        Vector3 offset = Vector3.zero;
        switch (trigger)
        {
            case "NorthTrigger": offset = new Vector3(0f, 0f, 50f); break;
            case "SouthTrigger": offset = new Vector3(0f, 0.0f, -50f); break;
            case "WestTrigger": offset = new Vector3(-50f, 0f, 0f); break;
            case "EastTrigger": offset = new Vector3(50f, 0f, 0f);  break;
        }
        currentRoom = Instantiate(levelBlock, currentRoom.transform.position + offset, Quaternion.identity);
        currentRoom.name = "newLevel";
    }

    public void CheckPlayerDistanceToPreviousBlock()
    {
        GameObject player = GameObject.FindWithTag("Player");
        wall = GameObject.Find("previousLevel/OuterWalls/Back");
        //If a new Levelblock has been created
        if (currentRoom && previousRoom)
        {
            //Check Distance to player
            //If my distance between the player and the wall is larger than 5, AND the player's z position is smaller than the walls z.position (smaller than -25)
            if (Vector3.Distance(player.transform.position, wall.transform.position) > 5 && player.transform.position.z < (wall.transform.position.z))
            {
                Destroy(previousRoom.gameObject);
                fae = FindObjectsOfType<FairyScript>().ToList<FairyScript>();
            }
        }     
        
    }

    void CheckTrigger(string enteredTriggerTag)
    {
        Debug.Log("Checking Trigger");
        Debug.Log(enteredTriggerTag);
        checkedTrigger = true;
        switch (enteredTriggerTag)
        {
            case "NorthTrigger": activatedTrigger = 0; /*wall = GameObject.Find("previousLevel/OuterWalls/Front");*/ break;
            case "SouthTrigger": activatedTrigger = 2; /*wall = GameObject.Find("previousLevel/OuterWalls/Back");*/ break;
            case "WestTrigger": activatedTrigger = 1; /*wall = GameObject.Find("previousLevel/OuterWalls/Left");*/ break;
            case "EastTrigger": activatedTrigger = 3; /*wall = GameObject.Find("previousLevel/OuterWalls/Right");*/ break;
        }

    }

    public void ClearFaeArray()
    {
        //Array.Clear(fae, 0, fae.Length);
        //fae.Clear();
        fae = FindObjectsOfType<FairyScript>().ToList();
        for (int i = fae.Count - 1; i >= 0; i--)
        {
            if (fae[i] == null)
                fae.RemoveAt(i);
        }
    }
    void LoadWinCase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        rightChoice = 0;
    }

    void LoadLoseCase()
    {
        SceneManager.LoadScene("LoseScene", LoadSceneMode.Single);
        wrongChoice = 0;
    }

    
}
