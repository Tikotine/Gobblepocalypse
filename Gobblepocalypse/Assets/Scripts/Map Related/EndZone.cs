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

    private void Start()
    {
        sc = FindObjectOfType<SceneController>();
        sm = FindObjectOfType<StarManager>();
        starCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkScene(SceneManager.GetActiveScene().name);
            sc.starAmt = starCount;
            sc.DoTransition(sceneName, musicEnum);
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
