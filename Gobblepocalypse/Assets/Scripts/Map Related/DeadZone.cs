using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private CheckpointManager cm;

    public void Awake()
    {
        cm = GameObject.FindWithTag("CheckpointManager").GetComponent<CheckpointManager>();     //Find Checkpoint Manager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cm.MoveToCheckpoint();      //Move Player to checkpoint
            cm.RetryColelctablesReset();    //Reset Collectables
        }
    }
}