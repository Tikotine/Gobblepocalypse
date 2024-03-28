using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    private SceneController sc;
    public GameObject[] images;
    public GameObject nextlvlButton;

    private void Start()
    {
        sc = FindObjectOfType<SceneController>();
        showImgs();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.levelComplete, transform.position);
        checkIfLvl3();
    }

    private void showImgs()
    {
        if (sc.starAmt == 0)
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
}
