using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerWallBounce, collision.transform.position);   //Play sound at collision location
        }
    }
}
