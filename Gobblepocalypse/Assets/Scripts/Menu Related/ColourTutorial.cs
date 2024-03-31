using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourTutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            TutorialManager.instance.disableTutorial = true;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.collectableCollected, collision.transform.position);   //Play sound at collectable location
            TutorialManager.instance.FinishTutorial();
        }
    }

    private void DelayFinishTutorial()
    {
        TutorialManager.instance.FinishTutorial();
        Destroy(this);
    }
}
