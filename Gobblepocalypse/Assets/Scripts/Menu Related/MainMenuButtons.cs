using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    private enum ButtonType
    {
        PLAY,
        LEVEL1,
        LEVEL2,
        LEVEL3,
        PLAYBACK,
        SETTINGS,
        QUIT,
        YES,
        NO
    }

    [SerializeField] private ButtonType buttonType;

    public float maxPushForce, minPushForce;
    public Vector2 pushForce;

    public GameObject player;
    public PlayerScript playerScript;
    public GameObject[] menuButtons;
    public GameObject mainMenuManagerObject;
    private MainMenuManager mainMenuManagerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        pushForce = new Vector2 (Random.Range(minPushForce, maxPushForce), Random.Range(minPushForce, maxPushForce));
        mainMenuManagerObject = GameObject.FindWithTag("MainMenuManager");
        mainMenuManagerScript = mainMenuManagerObject.GetComponent<MainMenuManager>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && playerScript.isAttacking)
        {
            PlayButton(buttonType);
        }
    }

    private void PlayButton(ButtonType bt)
    {
        switch (buttonType)
        {
            case ButtonType.PLAY:
                Debug.Log("PLAY");
                RemoveMenu();
                Invoke("InvokeSpawnLevelMenu", 2f);
                break;

            case ButtonType.LEVEL1:
                Debug.Log("Load LEVEL 1");
                break;

            case ButtonType.LEVEL2:
                Debug.Log("Load LEVEL 2");
                break;

            case ButtonType.LEVEL3:
                Debug.Log("Load LEVEL 3");
                break;

            case ButtonType.PLAYBACK:
                Debug.Log("PLAY BACK");
                RemoveMenu();
                Invoke("InvokeSpawnMainMenu", 2f);
                break;

            case ButtonType.SETTINGS:
                Debug.Log("SETTINGS");
                RemoveMenu();
                mainMenuManagerScript.TogglePlayerScript();
                mainMenuManagerScript.ActivateSettingsMenu();
                break;

            case ButtonType.QUIT:
                Debug.Log("QUIT");
                RemoveMenu();
                Invoke("InvokeSpawnQuitMenu", 2f);
                break;

            case ButtonType.YES:
                Debug.Log("YES");
                //Quit Game
                break;

            case ButtonType.NO:
                Debug.Log("NO");
                RemoveMenu();
                Invoke("InvokeSpawnMainMenu", 2f);
                break;

            default:
                Debug.LogWarning("Button type not supported: " + buttonType);
                break;
        }
    }

    public void RemoveMenu()
    {
        transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(pushForce);

        if (menuButtons != null)
        {
            foreach (GameObject e in menuButtons)
            {
                e.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        Invoke("DestroyParent", 5f);
    }

    public void DestroyParent()
    {
        //transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Destroy(transform.parent.gameObject);
    }

    #region Scuff
    public void InvokeSpawnMainMenu()
    {
        mainMenuManagerScript.SpawnMainMenu();
    }

    public void InvokeSpawnLevelMenu()
    {
        mainMenuManagerScript.SpawnLevelMenu();
    }

    public void InvokeSpawnQuitMenu() 
    {
        mainMenuManagerScript.SpawnQuitMenu();
    }

    #endregion

}
