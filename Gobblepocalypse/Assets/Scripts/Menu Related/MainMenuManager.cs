using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("References")]
    private GameObject mainCamera;
    private Camera cam;

    [Header("Tutorial Stuff")]
    public Color[] backgroundColours;
    public GameObject rightClick;
    public GameObject leftClick;
    public bool hasRightClicked;
    public bool hasLeftClicked;

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
        mainCamera = GameObject.FindWithTag("MainCamera");
        cam = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasRightClicked && Input.GetKeyDown(KeyCode.Mouse1))
        { 
            FinishRightClickTutorial();
        }

        if (!hasLeftClicked && hasRightClicked && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FinishLeftClickTutorial();
        }
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

    public void PlayButtonSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPress, transform.position);   //Play sound at button location
    }

    public void FinishRightClickTutorial()
    {
        hasRightClicked = true;
        rightClick.SetActive(false);
        leftClick.SetActive(true);
    }

    public void FinishLeftClickTutorial()
    {
        hasLeftClicked = true;
        leftClick.SetActive(false);
        cam.backgroundColor = backgroundColours[1];
        SpawnMainMenu();
    }
}
