using FMODUnity;
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

    //sprites
    public Sprite[] bossHeadSprites; //0 chase, 1 pre, 2 atk
    private SpriteRenderer sr;

    //Audio/FMOD
    private StudioEventEmitter emitter;

    private void Start()
    {
        boss = GetComponentInParent<Boss>();
        cam = FindObjectOfType<Camera>();
        cm = FindObjectOfType<CheckpointManager>();
        ps = FindObjectOfType<PlayerScript>();
        sr = GetComponent<SpriteRenderer>();
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.bossMovement, gameObject);    //Initialise the collectableIdle event at this gameobject
        emitter.Play();     //Start playing the audio
    }

    private void FixedUpdate()
    {
        //always follow player yaxis
        transform.position = Vector2.Lerp(transform.position, getCamYPos(), Time.deltaTime * 4);
        //make ui follow
        BossUI.transform.position = Vector2.Lerp(BossUI.transform.position, new Vector2(cam.transform.position.x, transform.position.y-2), 10);
        changeSprite();
    }

    private void changeSprite()
    {
        switch (boss.currentState.ToString())
        {
            case "BossChase":
                sr.sprite = bossHeadSprites[0];
                break;
            case "BossAttack":
                sr.sprite = bossHeadSprites[2];
                break;
            case "BossInterrupt":
                sr.sprite = bossHeadSprites[0];
                break;
            case "BossCharge":
                sr.sprite = bossHeadSprites[1];
                break;
        }
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
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerBossInterrupt, ps.transform.position);   //Play sound at boss location
                AudioManager.instance.PlayOneShot(FMODEvents.instance.bossInterrupted, transform.position);   //Play sound at boss location
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
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, ps.transform.position);   //Play sound at boss location
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
