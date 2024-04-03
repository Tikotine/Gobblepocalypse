using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //Data
    public int checkpointNo;    //In case need to track the individual checkpoints
    public GameObject player;
    public CheckpointManager cm;
    private SpriteRenderer sr;
    private ParticleSystem ps;
    public Sprite activatedSprite;

    //public BossManager bm;
    private bool checkpointActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");      //Get player based off tag
        cm = GameObject.FindWithTag("CheckpointManager").GetComponent<CheckpointManager>();     //Get the checkpointmanager based off tag
        sr = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
        /*var bossGO = GameObject.FindWithTag("BossManager");
        if (bossGO)
        {
            bm = bossGO.GetComponent<BossManager>();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (checkpointActive == false)
            {
                //SoundManager.instance.PlaySFX("CheckpointActivateSFX", 0.5f);     //Plays a sound on checkpoint activation, might not need cause we are probs making checkpoints seemless
            }

            cm.SetCheckpoint(checkpointNo);     //Sets the checkpoint using the checkpointmanager
            AudioManager.instance.PlayOneShot(FMODEvents.instance.checkpointActivated, transform.position);   //Play sound at collision location
            GetComponent<BoxCollider2D>().enabled = false;
            sr.sprite = activatedSprite;
            ps.Play();

            /*if (bm != null)
            {
                bm.SetBossSpawnPoint(checkpointNo);     //Boss manager stuff
            }*/
        }
    }
}
