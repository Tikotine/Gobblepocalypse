using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiked : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerSpiked, collision.gameObject.transform.position);   //Play sound at collision location
            Debug.Log("Play");
        }
    }
}
