using UnityEngine;
using UnityEngine.UI;

public class CheckpointManager : MonoBehaviour
{
    //References
    public GameObject player;

    //Checkpoints
    public GameObject[] checkpointWaypoints;
    public Vector2[] checkpointPos;

    //THE Checkpoint
    public Vector3 currentCheckpoint;

    //Collectables
    public GameObject collectablePrefab;
    public GameObject[] collectablesOnScreen;
    public Vector3[] collectablesLocation;

    //Bar
    public Slider bar;
    public float barProgress;

    //Platforms
    public GameObject[] platformList;

    //Boss stuff
    public GameObject boss;
    private Boss bs;
    public float bossRepsawnDistance;

    // Start is called before the first frame update
    void Awake()
    {
        platformList = GameObject.FindGameObjectsWithTag("ColourPlatform");
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        bs = FindObjectOfType<Boss>();

        barProgress = bar.value;

        if (checkpointWaypoints != null)  //If checkpoint waypoint array is not empty
        {
            for (int i = 0; i < checkpointWaypoints.Length; i++)  //For every prefab in the array
            {
                    checkpointPos[i].x = checkpointWaypoints[i].transform.position.x;
                    checkpointPos[i].y = checkpointWaypoints[i].transform.position.y;
            }

            if (collectablesOnScreen != null)   //If there are collectables
            {
                for (int i = 0; i < collectablesOnScreen.Length; i++)   //Record location of each collectable so they can respawn
                {
                    collectablesLocation[i] = collectablesOnScreen[i].transform.position;
                }
            }

            currentCheckpoint = checkpointPos[0];
        }

    }

    public void SetCheckpoint(int i)    //Method to update the current checkpoint
    {
        currentCheckpoint = new Vector3(checkpointPos[i].x, checkpointPos[i].y, 0f);
        barProgress = bar.value;
        //TP boss a specific distance behind checkpoint
    }

    public void MoveToCheckpoint()  //method to move the player to current checkpoint when respawning
    {
        player.transform.position = currentCheckpoint;

        foreach (GameObject n in platformList)
        {
            ColourPlatforms cp = n.GetComponent<ColourPlatforms>();
            cp.targetPoint = 1 - barProgress;
        }

        bar.value = barProgress;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void BossCheckpointReset()
    {
        bs.resetPos(currentCheckpoint.x - bossRepsawnDistance);
    }

    public void RetryColelctablesReset()        //Reset all collectables on death
    {
        for (int i = 0; i < collectablesOnScreen.Length; i++)
        {
            if (collectablesOnScreen[i] == null)
            {
                GameObject newPrefab = Instantiate(collectablePrefab, collectablesLocation[i], Quaternion.identity);
                Debug.Log("Collectable Respawned " + i);
                collectablesOnScreen[i] = newPrefab;
            }
        }
    }
     //Figure out how the collectables are going to respawn
}
