using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    public Vector3 pushForce;
    private BoxCollider2D bc;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TutorialManager.instance.disableTutorial = true;
            YeetBoss();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerBossInterrupt, collision.transform.position);   //Play sound at collectable location
            AudioManager.instance.PlayOneShot(FMODEvents.instance.bossInterrupted, transform.position);   //Play sound at collectable location
            Invoke("DelayFinishTutorial", 3f);
        }
    }

    private void YeetBoss()
    {
        bc.isTrigger = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(pushForce, ForceMode2D.Impulse);
    }

    private void DelayFinishTutorial()
    { 
        TutorialManager.instance.FinishTutorial();
        Destroy(this);
    }
}
