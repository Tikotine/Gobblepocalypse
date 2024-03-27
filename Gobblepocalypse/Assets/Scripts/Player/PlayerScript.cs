using TMPro;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

public class PlayerScript : MonoBehaviour
{
    //Values
    [field: Header("Changable Values")]
    public float shootPower;
    public int maxCharges;
    public int charges;
    public float chargeDuration;
    private float chargeTimer;

    //Boolean Controls
    [field: Header("Boolean Controls")]
    public bool canShoot = true;
    public bool isShooting = false;
    public bool isCharging = false;
    public bool chargeTimerActive = false;
    public bool isAttacking = false;
    public bool attackTimerActive = false;
    public bool attackCooldownTimerActive = false;
    public bool groundContact => Physics2D.OverlapBox(feetCol.transform.position, feetCol.size, 0, groundLayers);

    //Vector Inputs
    [field: Header("Vector Inputs")]
    public Vector3 mousePos;
    public Vector2 mouseDistance;
    public Vector3 downwardForce;

    //Player Refs
    [field: Header("Player References")]
    private Rigidbody2D rb;
    public BoxCollider2D feetCol;
    public LayerMask groundLayers;
    private PlayerFace pf;

    //Line
    [field: Header("Line References")]
    public LineRenderer lr;

    //Attacking
    [field: Header("Player Attacking")]
    public float attackDuration;
    public float attackTimer;
    public float attackCooldownDuration;
    public float attackCooldownTimer;

    //Attack UI
    [field: Header("Player Attacking UI")]
    public Image attackButton;
    public Image attackUICooldown;
    public TMP_Text attackTextCooldown;

    //Charging UI
    [field: Header("Player Charging UI")]
    //public TextMeshProUGUI text;
    public TMP_Text numberOfChargeText;
    public GameObject chargingBar;
    public Slider chargingSlider;

    //Colours
    [field: Header("Colours")]
    private SpriteRenderer sr;
    public Color chargingColour;
    public Color attackingColour;
    public Color attackCooldownColor;
    public Color defaultColour;
    public Color buttonDefaultColour;

    //Particles
    [field: Header("Particles")]
    public GameObject collisionParticlesObject;
    private CollisionParticles cp;
    public GameObject chargeParticlesObject;

    //Audio
    [field: Header("Player Audio")]
    private EventInstance playerRoll;

    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();   //Reference the players rigidbody2D
        lr = gameObject.GetComponent<LineRenderer>();   //Reference the player's line renderer
        sr = GetComponent<SpriteRenderer>();
        pf = GetComponentInChildren<PlayerFace>();
        cp = collisionParticlesObject.GetComponent<CollisionParticles>();
        lr.enabled = false;

        chargingSlider.maxValue = chargeDuration;
        chargingBar.SetActive(false);

        attackTextCooldown.gameObject.SetActive(false); //Hide cooling down UI
        attackUICooldown.fillAmount = 0.0f;
    }

    private void Start()
    {
        playerRoll = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerRoll);     //Initalize instance for roll audio
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();

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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerShoot, transform.position);   //Play sound at collectable location
            pf.changeFace(1);
        }

        #endregion

        #region Charging
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isShooting)    //On Right click
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerCharge, transform.position);   //Play sound at player location
        }

        if (Input.GetKey(KeyCode.Mouse1) && !isShooting)    //On right mouse hold
        {
            canShoot = false;
            chargeTimerActive = true;       //Toggle all booleans
            pf.changeFace(3);
            if (isAttacking != true)
            {
                sr.color = chargingColour;
            }


            if (isCharging == false)
            {
                Debug.Log("Stopped");
                chargeParticlesObject.GetComponent<ParticleSystem>().Play();
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
            pf.changeFace(4);

            if (isAttacking != true)
            {
                sr.color = defaultColour;
            }

            StopCharging();
        }

        if (chargeTimerActive == true && isCharging == true)    //If charge timer active and player is charging
        {
            chargeTimer += Time.deltaTime;      //Add time passed to chargeTimer

            chargingBar.SetActive(true); //Show charging UI
            chargingSlider.value = chargeTimer;
            
            if (chargeTimer >= chargeDuration)  //If player successfully charges for charge duration
            {
                if (charges < maxCharges)   //If charges is less than max amount
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerCharge, transform.position);   //Play sound at player location
                    charges++;      //Add a charge
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerGainCharge, transform.position);   //Play sound at collectable location
                    chargeTimer = 0;    //Reset timer

                    chargingSlider.value = chargeTimer;
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
            pf.changeFace(2);
            attackButton.color = attackingColour;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerAttackModeActivate, transform.position);   //Play sound at collectable location
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

                attackButton.color = buttonDefaultColour;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerAttackModeCooldown, transform.position);   //Play sound at collectable location
            }
        }

        if (isAttacking == false && attackCooldownTimerActive == true)   //If the cooldown timer is on
        {
            if (attackCooldownTimer < attackCooldownDuration)
            { 
                attackCooldownTimer += Time.deltaTime;  //increase timer by time passed until duration is hit
                sr.color = attackCooldownColor;

                attackTextCooldown.gameObject.SetActive(true);
                attackTextCooldown.text = Mathf.RoundToInt(attackCooldownDuration - attackCooldownTimer).ToString(); //Change text on cooldown UI
                attackUICooldown.fillAmount = (attackCooldownDuration - attackCooldownTimer) / attackCooldownDuration; //adjust fill amount on cooldown UI
            }

            if (attackCooldownTimer >= attackCooldownDuration)
            {
                attackCooldownTimerActive = false;  //Turn off the cooldown boolean
                attackCooldownTimer = 0;    //Reset timer
                sr.color = defaultColour;
                AudioManager.instance.PlayOneShot(FMODEvents.instance.playerAttackModeReady, transform.position);   //Play sound at collectable location

                attackTextCooldown.gameObject.SetActive(false); //Hide cooling down UI
                attackUICooldown.fillAmount = 0.0f;
            }
        }
        #endregion

        numberOfChargeText.text = charges.ToString();
    }

    public void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SolidPlatform" || collision.gameObject.tag == "ColourPlatform")
        {
            GameObject impact = Instantiate (collisionParticlesObject, transform.position, Quaternion.identity) as GameObject;      //On impact, spawn particles at collision point
            impact.GetComponent<CollisionParticles>().PlayParticles(collision);     //Run the function
        }
    }

    public void ResetTimers()
    { 
        attackTimer = 0;
        attackCooldownTimer = attackCooldownDuration;
    }

    public void ResetVelocity()
    {
        rb.freezeRotation = true;
        rb.velocity = Vector3.zero;
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,0);
        rb.freezeRotation = false;
    }

    public void StopCharging()
    {
        chargingSlider.value = 0;
        chargingBar.SetActive(false);
        chargeParticlesObject.GetComponent<ParticleSystem>().Stop();
    }

    private void UpdateSound()
    {
        //Start footsteps event if player has a velocity and is on the ground
        if ((rb.velocity.x > 1 || rb.velocity.x < -1) && groundContact)
        {
            //Get the playback state, we dont want it to play if it is already playing
            PLAYBACK_STATE playbackState;
            playerRoll.getPlaybackState(out playbackState);

            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))   //If playback is currently stopped, play it
            {
                playerRoll.start();
            }

            else    //Else stop playback
            {
                playerRoll.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        
    }
}
