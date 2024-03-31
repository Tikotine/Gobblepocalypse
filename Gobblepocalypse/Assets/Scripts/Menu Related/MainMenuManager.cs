using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    //Button Prefabs
    [Header("Menu Button Prefabs")]
    public GameObject mainMenuButtonsPrefab;
    public GameObject levelButtonsPrefab;
    public GameObject quitButtonsPrefab;
    public GameObject settingsMenu;

    [Header("Player")]
    public GameObject player;
    public PlayerScript playerScript;

    [Header("Timers")]
    private SceneController sc;
    public GameObject timersCanvas;
    public GameObject timer1;
    public GameObject timer2;
    public GameObject timer3;
    private string timeString1;
    private string timeString2;
    private string timeString3;
    public TextMeshProUGUI TimerTxt1;
    public TextMeshProUGUI TimerTxt2;
    public TextMeshProUGUI TimerTxt3;

    // Start is called before the first frame update
    void Start()
    {
        sc = FindObjectOfType<SceneController>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        TutorialManager.instance.CheckTutorialStatus();
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMainMenu()
    { 
        timersCanvas.SetActive(false);
        Instantiate(mainMenuButtonsPrefab);
        StarManager.instance.RemoveStars();
    }

    public void SpawnLevelMenu()
    {
        timersCanvas.SetActive(true);
        Instantiate(levelButtonsPrefab);
        StarManager.instance.UpdateStars();
    }

    public void SpawnQuitMenu()
    {
        timersCanvas.SetActive(false);
        Instantiate(quitButtonsPrefab);
        StarManager.instance.RemoveStars();
    }

    public void ActivateSettingsMenu()
    {
        timersCanvas.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void TogglePlayerScript()
    {
        if (playerScript.enabled)
        {
            playerScript.enabled = false;
        }

        else
        {
            playerScript.enabled = true;
        }
    }

    public void PlayButtonSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, transform.position);   //Play sound at button location
    }

    public void UpdateTimer()
    {
        if (sc.Lvl1Best != 0)
        {
            timer1.SetActive(true);

            float minutes1 = Mathf.FloorToInt(sc.Lvl1Best / 60);
            float seconds1 = Mathf.FloorToInt(sc.Lvl1Best % 60);

            timeString1 = string.Format("{0:00}:{1:00}", minutes1, seconds1);
            TimerTxt1.text = timeString1;
        }

        if (sc.Lvl2Best != 0)
        { 
            timer2.SetActive(true);

            float minutes2 = Mathf.FloorToInt(sc.Lvl2Best / 60);
            float seconds2 = Mathf.FloorToInt(sc.Lvl2Best % 60);

            timeString2 = string.Format("{0:00}:{1:00}", minutes2, seconds2);
            TimerTxt2.text = timeString2;
        }

        if (sc.Lvl3Best != 0)
        {
            timer3.SetActive(true);

            float minutes3 = Mathf.FloorToInt(sc.Lvl3Best / 60);
            float seconds3 = Mathf.FloorToInt(sc.Lvl3Best % 60);

            timeString3 = string.Format("{0:00}:{1:00}", minutes3, seconds3);
            TimerTxt3.text = timeString3;
        }
    }
}
