using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
//jme
public class Boss : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject player;

    public BoxCollider2D interruptHitbox;
    public BoxCollider2D atkHitbox;

    public BossStates currentState;

    public bool canInterrupt;
    public bool canMove;

    public float currentMoveSpeed;
    public float normalMoveSpeed;
    public float fastMoveSpeed;
    public float maxDistance;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        SetCurrentState(new BossChase(this));
        interruptHitbox.enabled = false;
        atkHitbox.enabled= false;
    }

    private void FixedUpdate()
    {
        /*if(canMove)
        {
            MoveToPlayer(); 
        }*/
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

    /*private void OnBecameInvisible()
    {
        Debug.Log("Boss OOR");
        currentMoveSpeed = fastMoveSpeed;
    }

    private void OnBecameVisible()
    {
        Debug.Log("Boss IR");
        currentMoveSpeed = normalMoveSpeed;
    }*/

    private float checkDistToPlayer()
    {
        return player.transform.position.x - gameObject.transform.position.x;
    }

    #endregion

    public void attack()
    {

    }

    public void resetPos(float checkpointXaxis)
    {
        transform.position = new Vector2(checkpointXaxis, transform.position.y);
    }
}
