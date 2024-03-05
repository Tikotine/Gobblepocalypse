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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMainMenu()
    { 
        Instantiate(mainMenuButtonsPrefab);
    }

    public void SpawnLevelMenu()
    {
        Instantiate(levelButtonsPrefab);
    }

    public void SpawnQuitMenu()
    {
        Instantiate(quitButtonsPrefab);
    }

    public void ActivateSettingsMenu()
    {
        settingsMenu.SetActive(true);
    }
}
