using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isShooting = false;
    public bool isCharging = false;
    public bool chargeTimerActive = false;
    public bool isAttacking = false;
    public bool attackTimerActive = false;
    public bool attackCooldownTimerActive = false;

    //Vector Inputs
    public Vector3 mousePos;
    public Vector2 mouseDistance;
    public Vector3 downwardForce;

    //Player Refs
    private Rigidbody2D rb;

    //Line
    public LineRenderer lr;

    //Attacking
    public float attackDuration;
    public float attackTimer;
    public float attackCooldownDuration;
    public float attackCooldownTimer;

    //Charge Count
    public TextMeshProUGUI text;

    //Colours
    private SpriteRenderer sr;
    public Color chargingColour;
    public Color attackingColour;
    public Color attackCooldownColor;
    public Color defaultColour;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();   //Reference the players rigidbody2D
        lr = gameObject.GetComponent<LineRenderer>();   //Reference the player's line renderer
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        #region Shooting
        if (Input.GetKey(KeyCode.Mouse0) && !isCharging)   //If can shoot is true and left mouse is held
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
                isShooting = true;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //Detect the mouse position with reference to main camera
                mouseDistance = mousePos - transform.position;      //Calculate the distance from the mouse to the game object
                #endregion
                //Set line renderer here
                #region Calculate Line
                lr.enabled = true;  //Enable Line Renderer
                lr.SetPosition(0, transform.position);
                lr.SetPosition(1, mousePos);
                #endregion
            }
        }

        if (canShoot == true && isCharging == false && Input.GetKeyUp(KeyCode.Mouse0)) //On left mouse release
        {
            lr.enabled = false; //Disable Line Renderer
            rb.AddForce(mouseDistance * shootPower, ForceMode2D.Impulse);  //add a force to player in direction of mouse
            charges--;  //use a charge
            isShooting = false;
        }
        #endregion

        #region Charging
        if (Input.GetKey(KeyCode.Mouse1) && !isShooting)    //On right mouse hold
        { 
            canShoot = false;
            chargeTimerActive = true;       //Toggle all booleans
            sr.color = chargingColour;

            if (isCharging == false)
            {
                Debug.Log("Stopped");
                rb.velocity = Vector3.zero;     //Set velocity of player to 0
                rb.AddForce(downwardForce, ForceMode2D.Impulse);
            }

            isCharging = true;
        }

        if (isCharging == true && Input.GetKeyUp(KeyCode.Mouse1))   //On right mouse release
        {
            canShoot = true;
            isCharging = false;
            chargeTimerActive = false;
            chargeTimer = 0;    //Reset charge timer
            sr.color = defaultColour;
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
        #endregion

        #region Attacking
        if (attackCooldownTimerActive == false && Input.GetKeyDown(KeyCode.Space) && isAttacking == false)   //If attack is not on cd and player presses space
        {
            //activate attack mode
            isAttacking = true;
            //Start attack timer
            attackTimer = attackDuration;   //Set the timer to its duration
            attackTimerActive = true;       //Begin the attack timer countdown
            sr.color = attackingColour; //Change colour here
        }

        if (attackTimerActive == true && isAttacking == true)   //If the attack timer is on
        {
            if (attackTimer > 0)
            { 
                attackTimer -= Time.deltaTime;  //Decrease timer until it hits 0
            }

            if (attackTimer <= 0)
            {
                isAttacking = false; //turn off attack mode
                attackTimerActive = false;  //Stop the countdown and activate the cooldown
                attackCooldownTimerActive = true;
            }
        }

        if (isAttacking == false && attackCooldownTimerActive == true)   //If the cooldown timer is on
        {
            if (attackCooldownTimer < attackCooldownDuration)
            { 
                attackCooldownTimer += Time.deltaTime;  //increase timer by time passed until duration is hit
                sr.color = attackCooldownColor;
            }

            if (attackCooldownTimer >= attackCooldownDuration)
            {
                attackCooldownTimerActive = false;  //Turn off the cooldown boolean
                attackCooldownTimer = 0;    //Reset timer
                sr.color = defaultColour;
            }
        }
        #endregion

        text.text = charges.ToString();
    }

    public void FixedUpdate()
    {

    }

    public void ResetTimers()
    { 
        attackTimer = 0;
        attackCooldownTimer = attackCooldownDuration;
    }

    public void ResetVelocity()
    { 
        rb.velocity = Vector3.zero;
    }
}
