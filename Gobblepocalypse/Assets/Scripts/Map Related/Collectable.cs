using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ColourPlatforms[] platformList;
    public float timeRestored;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (ColourPlatforms n in platformList)
            {

                n.targetPoint -= timeRestored / n.time;
            }

            Destroy(gameObject);
        }
    }
}
