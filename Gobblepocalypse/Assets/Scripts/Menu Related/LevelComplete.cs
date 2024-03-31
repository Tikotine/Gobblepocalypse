using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    private SceneController sc;
    public GameObject[] images;
    public GameObject nextlvlButton;
    public TextMeshProUGUI timer;
    public GameObject newhighscore;

    private void Start()
    {
        sc = FindObjectOfType<SceneController>();
        showImgs();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.levelComplete, transform.position);
        Times();
        checkIfLvl3();
    }

    private void showImgs()
    {
        if (sc.starAmt == 0)
        {
            /*for (int i = 0; i < sc.starAmt; i++)
            {
                images[i].GetComponent<Image>().color = Color.black;
            }*/
        }
        else
        {
            for (int i = 0; i < sc.starAmt; i++)
            {
                //images[i].SetActive(true);
                images[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void mainMenu()
    {
        sc.DoTransition("MainMenu", MusicControl.MENU);
        TutorialManager.instance.CheckTutorialStatus();
        sc.starAmt = 0;
    }

    public void nextLvl()
    {
        sc.DoTransitionInt(sc.lastSceneBuildIndex+1, (MusicControl)(sc.lastSceneBuildIndex+1));
        sc.starAmt = 0;
    }

    public void checkIfLvl3()
    {
        if(sc.lastSceneBuildIndex == 3)
        {
            //dont show next lvl button
            nextlvlButton.SetActive(false);
        }
        else
        {
            nextlvlButton.SetActive(true);
        }
    }

    private void Times()
    {
        //check which prev scene
        //switchcase all of it... smh
        float tempTime = 0;
        
        switch (sc.lastSceneBuildIndex)
        {
            case 1:
                tempTime = sc.Lvl1time;
                break; 

            case 2:
                tempTime = sc.Lvl2time;
                break;
                
            case 3:
                tempTime = sc.Lvl3time;
                break;
        }

        float minutes = Mathf.FloorToInt(tempTime / 60);
        float seconds = Mathf.FloorToInt(tempTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (sc.NewBest)
        {
            newhighscore.SetActive(true);
        }
        else
        {
            newhighscore.SetActive(false);
        }

        sc.NewBest = false;
    }
}
