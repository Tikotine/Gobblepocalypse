using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]      //Require a studioeventemitter
public class Collectable : MonoBehaviour
{
    public GameObject[] platformList;
    public float timeRestored;

    //Audio/FMOD
    private StudioEventEmitter emitter;

    // Start is called before the first frame update
    void Awake()
    {
        platformList = GameObject.FindGameObjectsWithTag("ColourPlatform");
    }

    private void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.collectableIdle, gameObject);    //Initialise the collectableIdle event at this gameobject
        emitter.Play();     //Start playing the audio
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollectCollectable();

            Destroy(gameObject);
        }
    }

    private void CollectCollectable()
    {
        foreach (GameObject n in platformList)      //Increase the timer count for all platforms
        {
            ColourPlatforms cp = n.GetComponent<ColourPlatforms>();
            cp.targetPoint -= timeRestored / cp.time;
            if (cp.targetPoint < 0)
            {
                cp.targetPoint = 0;
            }
        }

        emitter.Stop();     //Stop the idle emitter
        AudioManager.instance.PlayOneShot(FMODEvents.instance.collectableCollected, transform.position);   //Play sound at collectable location
    }
}
