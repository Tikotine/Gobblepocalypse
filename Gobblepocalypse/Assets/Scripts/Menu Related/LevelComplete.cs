using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private SceneController sc;
    public string nextLevelName;
    public MusicControl nextLevelMusicEnum;
    public GameObject[] images;

    private void Start()
    {
        sc = FindObjectOfType<SceneController>();
        showImgs();
    }

    private void showImgs()
    {
        if(sc.starAmt == 0)
        {
            //sadge
        }
        else
        {
            for (int i = 0; i < sc.starAmt; i++)
            {
                images[i].SetActive(true);
            }
        }
    }

    public void mainMenu()
    {
        sc.DoTransition("MainMenu", MusicControl.MENU);
        sc.starAmt = 0;
    }

    public void nextLvl()
    {
        sc.DoTransition(nextLevelName, nextLevelMusicEnum);
        sc.starAmt = 0;
    }
}
