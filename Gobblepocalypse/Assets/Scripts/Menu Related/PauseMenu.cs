using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public GameObject bg;
    private GameObject player;
    private PlayerScript ps;
    public bool isPaused = false;
    private SceneController sc;
    private bool doOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuObject.SetActive(false);
        sc = FindObjectOfType<SceneController>();
        player = GameObject.FindWithTag("Player");
        ps = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }

            else 
            { 
                PauseGame();
            }
        }
    }
        
    private void PauseGame()
    {
        ps.enabled = false;
        bg.SetActive(true);
        pauseMenuObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        ps.enabled = true;
        bg.SetActive(false);
        pauseMenuObject.SetActive(false);
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void mainMenu()
    {
        if(!doOnce)
        {
            ResumeGame();
            sc.DoTransition("MainMenu", MusicControl.MENU);
            AudioManager.instance.StopAmbience();
            TutorialManager.instance.CheckTutorialStatus();
            sc.starAmt = 0;
            doOnce = true;
        }
    }
}
