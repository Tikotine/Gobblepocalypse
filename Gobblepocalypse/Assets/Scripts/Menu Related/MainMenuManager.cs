using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnMainMenu()
    { 
        Instantiate(mainMenuButtonsPrefab);
        StarManager.instance.RemoveStars();
    }

    public void SpawnLevelMenu()
    {
        Instantiate(levelButtonsPrefab);
        StarManager.instance.UpdateStars();
    }

    public void SpawnQuitMenu()
    {
        Instantiate(quitButtonsPrefab);
        StarManager.instance.RemoveStars();
    }

    public void ActivateSettingsMenu()
    {
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
}
