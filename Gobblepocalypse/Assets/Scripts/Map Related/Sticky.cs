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
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && isSticking == true)
        {
            collision.gameObject.transform.position = stickLocation;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        { 
            isSticking = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isSticking = false;
        }
    }
}
