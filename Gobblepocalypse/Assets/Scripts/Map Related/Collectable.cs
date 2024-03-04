using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject[] platformList;
    public float timeRestored;

    // Start is called before the first frame update
    void Awake()
    {
        platformList = GameObject.FindGameObjectsWithTag("ColourPlatform");
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

        AudioManager.instance.PlayOneShot(FMODEvents.instance.collectableCollected, transform.position);   //Play sound at collectable location
    }
}
