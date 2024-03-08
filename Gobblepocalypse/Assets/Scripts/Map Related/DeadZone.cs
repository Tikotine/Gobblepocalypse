using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private CheckpointManager cm;
    private Boss bossScript;

    public void Awake()
    {
        cm = GameObject.FindWithTag("CheckpointManager").GetComponent<CheckpointManager>();     //Find Checkpoint Manager
        bossScript = GameObject.FindWithTag("Boss").GetComponent<Boss>();     //Find Boss script
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cm.MoveToCheckpoint();      //Move Player to checkpoint
            cm.RetryColelctablesReset();    //Reset Collectables
            cm.BossCheckpointReset();   //Reset boss to last checkpoint
            cm.player.GetComponent<PlayerScript>().ResetTimers();   //Reset attackmode timers
            cm.player.GetComponent<PlayerScript>().ResetVelocity(); //Reset player velocity
            bossScript.SetCurrentState(new BossChase(bossScript));      //Reset boss state
        }
    }
}
