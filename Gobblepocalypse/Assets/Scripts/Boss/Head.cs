using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//jme
public class Head : MonoBehaviour
{
    public Boss boss;
    public Camera cam;

    private CheckpointManager cm;
    private PlayerScript ps;

    //Walls
    public Sticky solidSticky;
    public Sticky changeSticky;

    //UI canvas
    public GameObject BossUI;

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
        transform.position = Vector2.Lerp(transform.position, getCamYPos(), Time.deltaTime * 4);
        //make ui follow
        BossUI.transform.position = Vector2.Lerp(BossUI.transform.position, new Vector2(cam.transform.position.x, transform.position.y-2), 10);
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
            else if (isBossInInterrupt(boss.currentState.ToString()))
            {
                Debug.Log("boss in interrupted state?");
            }
            else
            {
                //if in preatk state false
                Debug.Log("player hit by boss");
                ResetStickyPlatforms();     //Unstick from platforms
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

    private bool isBossInInterrupt(string stateName)
    {
        switch (stateName)
        {
            case "BossInterrupt":
                return true;

            default:
                return false;
        }
    }

    private void ResetStickyPlatforms()
    {
        solidSticky.isSticking = false;
        changeSticky.isSticking = false;
    }

    private void OnBecameInvisible()
    {
        //show UI
        BossUI.SetActive(true);
    }

    private void OnBecameVisible()
    {
        //hide UI
        BossUI.SetActive(false);
    }
}
