using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//jme
public class Wall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player somehow hit wall so instakilled");
        }
    }
}
