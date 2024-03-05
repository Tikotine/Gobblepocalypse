using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuObject;
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuObject.SetActive(false);
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
        pauseMenuObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        pauseMenuObject.SetActive(false);
        settingsMenu.SetActive(false);
        quitMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1.0f;
    }
}
