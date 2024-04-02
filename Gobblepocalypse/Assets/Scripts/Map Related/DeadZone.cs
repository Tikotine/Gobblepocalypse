using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private CheckpointManager cm;
    private Boss bossScript;
    private SceneController sc;
    private GameObject player;

    public void Awake()
    {
        cm = GameObject.FindWithTag("CheckpointManager").GetComponent<CheckpointManager>();     //Find Checkpoint Manager
        bossScript = GameObject.FindWithTag("Boss").GetComponent<Boss>();     //Find Boss script
        sc = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            player.SetActive(false);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, cm.player.transform.position);   //Play sound at player location
            sc.transition.SetTrigger("Start");
            Invoke("Death", 1f);
        }
    }

    private void Death()
    {
        cm.MoveToCheckpoint();      //Move Player to checkpoint
        cm.RetryColelctablesReset();    //Reset Collectables
        cm.BossCheckpointReset();   //Reset boss to last checkpoint
        cm.player.GetComponent<PlayerScript>().ResetTimers();   //Reset attackmode timers
        cm.player.GetComponent<PlayerScript>().ResetVelocity(); //Reset player velocity
        bossScript.SetCurrentState(new BossChase(bossScript));      //Reset boss state
        player.SetActive(true);
        player.GetComponent<PlayerScript>().StopCharging();
    }
}
