using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//jme

public class EndZone : MonoBehaviour
{
    private SceneController sc;
    private StarManager sm;
    public string sceneName;
    public MusicControl musicEnum;
    private int starCount;
    private TimerScript ts;

    private void Start()
    {
        sc = FindObjectOfType<SceneController>();
        sm = FindObjectOfType<StarManager>();
        ts = FindObjectOfType<TimerScript>();
        starCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            checkScene(SceneManager.GetActiveScene().name);
            sc.starAmt = starCount;
            ts.endTimer();
            sc.DoTransition(sceneName, musicEnum);
            AudioManager.instance.StopAmbience();
        }
    }

    private void checkScene(string name)
    {
        switch(name)
        {
            case "LEVEL1":
                for(int i = 0; i<3; i++)
                {
                    if (sm.starsCollected[i])
                    {
                        starCount++;
                    }
                }
                break;

            case "LEVEL2":
                for (int i = 3; i < 6; i++)
                {
                    if (sm.starsCollected[i])
                    {
                        starCount++;
                    }
                }
                break;

            case "LEVEL3":
                for (int i = 6; i < sm.starsCollected.Length; i++)
                {
                    if (sm.starsCollected[i])
                    {
                        starCount++;
                    }
                }
                break;
        }
    }
}
