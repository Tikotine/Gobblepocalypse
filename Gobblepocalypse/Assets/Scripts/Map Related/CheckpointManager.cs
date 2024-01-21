using UnityEngine;

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

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");

        if (checkpointWaypoints != null)  //If checkpoint waypoint array is not empty
        {
            for (int i = 0; i < checkpointWaypoints.Length; i++)  //For every prefab in the array
            {
                if (i == 0) //If first checkpoint, set spawn to beginning
                {
                    checkpointPos[i].x = 0f;
                    checkpointPos[i].y = 0f;
                }

                else  //Else set spawnpoint to the checkpoint waypoint
                {
                    checkpointPos[i].x = checkpointWaypoints[i].transform.position.x;
                    checkpointPos[i].y = checkpointWaypoints[i].transform.position.y;
                }
            }

            if (collectablesOnScreen != null)   //If there are collectables
            {
                for (int i = 0; i < collectablesOnScreen.Length; i++)   //Record location of each collectable so they can respawn
                {
                    collectablesLocation[i] = collectablesOnScreen[i].transform.position;
                }
            }
        }

    }

    public void SetCheckpoint(int i)    //Method to update the current checkpoint
    {
        currentCheckpoint = new Vector3(checkpointPos[i].x, checkpointPos[i].y, 0f);
    }

    public void MoveToCheckpoint()  //method to move the player to current checkpoint when respawning
    {
        player.transform.position = currentCheckpoint;
    }

    /*
    public void RetryColelctablesReset()
    {
        for (int i = 0; i < collectablesOnScreen.Length; i++)
        {
            if (collectablesCollected[i] == true && collectablesStored[i] == false)
            {
                Instantiate(collectablesPrefabs[i], collectablesLocation[i], Quaternion.identity);
                collectablesCollected[i] = false;
            }

            if (collectablesStored[i] == true)
            {
                collectablesCollected[i] = true;
            }
        }
    }
    */ //Figure out how the collectables are going to respawn
}
