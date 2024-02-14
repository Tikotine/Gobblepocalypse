using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//jme
public class Boss : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject player;

    public Camera cam;

    public BoxCollider2D interruptHitbox;
    public BoxCollider2D atkHitbox;
    public BoxCollider2D wallHitbox;

    public BossStates currentState;

    public bool canInterrupt;
    public bool canMove;
    public bool thrownPlayer = false;

    public float currentMoveSpeed;
    public float normalMoveSpeed;
    public float fastMoveSpeed;
    public float maxDistance;
    public float preAtkDuration = 3f;
    public float preAtkInterval = 20; //more like time before pre attack but yea i guess default is 20sec?
    public float stunDuration = 2;
    public float yeetForce = 15;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        cam = FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody2D>();
        SetCurrentState(new BossChase(this));
    }

    private void FixedUpdate()
    {
        currentState.DoActionUpdate(Time.fixedDeltaTime);
        currentMoveSpeed = Mathf.Lerp(normalMoveSpeed, fastMoveSpeed, Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) / maxDistance);
    }

    public void SetCurrentState(BossStates nextState) //SetCurrentState(new stateName(this));
    {
        if (currentState != nextState)
        {
            currentState = nextState;
        }
    }
    //check state -> currentState.ToString() == "stateName" 

    #region movement

    public void MoveToPlayer()
    {
        Vector3 bossMovement = new Vector3(currentMoveSpeed, 0f, 0f);
        gameObject.transform.Translate(bossMovement, Space.Self);
        //transform.position = Vector2.MoveTowards(transform.position, getPlayerPos(), currentMoveSpeed * Time.deltaTime);
    }
    
    private Vector2 getPlayerPos()
    {
        return new Vector2(player.transform.position.x, transform.position.y);
    }

    private float checkDistToPlayer()
    {
        return player.transform.position.x - gameObject.transform.position.x;
    }

    #endregion

    public void preAtk()
    {

    }

    public void attack()
    {
        //rush across scrn, uses cam coz its the viewport
        transform.position = Vector2.Lerp(transform.position, cam.transform.position, Time.deltaTime * 4);
    }

    public void reduceInterval(int amt)
    {
        preAtkInterval -= amt; // eg - 5 sec
    }

    public void resetPos(float checkpointXaxis)
    {
        transform.position = new Vector2(checkpointXaxis, transform.position.y);
    }

    public void throwPlayer()
    {
        player.transform.position = new Vector2(player.transform.position.x, transform.position.y);
        //sry i lazy aha
        player.GetComponent<Rigidbody2D>().velocity= Vector2.zero;
        player.GetComponent<Rigidbody2D>().AddForce(player.transform.right * yeetForce, ForceMode2D.Impulse); //always yeet out to the right so
        thrownPlayer = true;
    }
}
