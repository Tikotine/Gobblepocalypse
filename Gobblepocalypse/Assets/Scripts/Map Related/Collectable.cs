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
            foreach (GameObject n in platformList)
            {
                ColourPlatforms cp = n.GetComponent<ColourPlatforms>();
                cp.targetPoint -= timeRestored / cp.time;
            }

            Destroy(gameObject);
        }
    }
}
