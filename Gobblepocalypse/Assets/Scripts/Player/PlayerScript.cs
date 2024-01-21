using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Values
    public float shootPower;
    public int maxCharges;
    public int charges;
    public float chargeDuration;
    private float chargeTimer;

    //Boolean Controls
    public bool canShoot = true;
    public bool isCharging = false;
    public bool chargeTimerActive = false;

    //Vector Inputs
    public Vector3 mousePos;
    public Vector2 mouseDistance;

    //Player Refs
    private Rigidbody2D rb;



    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();   //Reference the players rigidbody2D
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))   //If can shoot is true and left mouse is held
        {
            if (charges > 0)
            {
                canShoot = true;
            }

            else 
            { 
                canShoot = false;
            }

            if (canShoot == true)
            {
                #region Calculate Shoot
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //Detect the mouse position with reference to main camera
                mouseDistance = mousePos - transform.position;      //Calculate the distance from the mouse to the game object
                #endregion
            }

            //Set line renderer here
        }

        if (canShoot == true && isCharging == false && Input.GetKeyUp(KeyCode.Mouse0)) //On left mouse release
        { 
            rb.AddForce(mouseDistance * shootPower, ForceMode2D.Impulse);  //add a force to player in direction of mouse
            charges--;  //use a charge
        }

        if (Input.GetKey(KeyCode.Mouse1))    //On right mouse hold
        { 
            canShoot = false;
            isCharging = true;
            chargeTimerActive = true;       //Toggle all booleans
            rb.velocity = Vector3.zero;     //Set velocity of player to 0
        }

        if (isCharging == true && Input.GetKeyUp(KeyCode.Mouse1))   //On right mouse release
        {
            canShoot = true;
            isCharging = false;
            chargeTimerActive = false;
            chargeTimer = 0;    //Reset charge timer
        }

        if (chargeTimerActive == true && isCharging == true)    //If charge timer active and player is charging
        {
            chargeTimer += Time.deltaTime;      //Add time passed to chargeTimer
            
            if (chargeTimer >= chargeDuration)  //If player successfully charges for charge duration
            {
                if (charges < maxCharges)   //If charges is less than max amount
                {
                    charges++;      //Add a charge
                    chargeTimer = 0;    //Reset timer
                }
            }
        }
    }

}
