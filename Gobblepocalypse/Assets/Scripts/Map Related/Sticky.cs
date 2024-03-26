using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    public Vector3 stickLocation;
    public bool isSticking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        { 
            stickLocation = collision.gameObject.GetComponent<Transform>().position;
            isSticking = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerWallStick, collision.transform.position);   //Play sound at collision location
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isSticking == true)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            collision.gameObject.transform.position = stickLocation;
        }

        if (Input.GetMouseButton(0))
        { 
            isSticking = false;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSticking = false;
            collision.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        }
    }

}
