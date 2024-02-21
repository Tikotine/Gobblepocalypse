using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Head : MonoBehaviour
{
    public Boss boss;
    public Camera cam;

    private CheckpointManager cm;
    private PlayerScript ps;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();   
        cam = FindObjectOfType<Camera>();
        cm = FindObjectOfType<CheckpointManager>();
        ps = FindObjectOfType<PlayerScript>();
    }

    private void FixedUpdate()
    {
        //always follow player yaxis
        transform.position = Vector2.Lerp(transform.position, getCamYPos(), Time.deltaTime*4);
    }

    private Vector2 getCamYPos()
    {
        return new Vector2(transform.position.x, cam.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isBossInPreAtk(boss.currentState.ToString()) && ps.isAttacking == true)
            {
                //if in preatk state true
                Debug.Log("player interrupted boss");
                boss.SetCurrentState(new BossInterrupt(boss));
            }
            else
            {
                //if in preatk state false
                Debug.Log("player hit by boss");
                cm.MoveToCheckpoint();      //Move Player to checkpoint
                cm.RetryColelctablesReset();    //Reset Collectables
                cm.BossCheckpointReset();   //Reset boss to last checkpoint
                ps.ResetTimers();   //Reset attackmode timers
                boss.SetCurrentState(new BossChase(boss));
                ps.ResetVelocity(); //Reset player velocity
            }
        }
    }

    private bool isBossInPreAtk(string stateName)
    {
        switch (stateName)
        {
            case "BossCharge":
                return true;

            default: 
                return false;
        }
    }
}
