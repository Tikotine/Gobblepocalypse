using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance { get; private set; }

    [Header("References")]
    private GameObject mainCamera;
    private Camera cam;

    [Header("Tutorial Stuff")]
    public Color[] backgroundColours;
    public GameObject rightClick;
    public GameObject leftClick;
    public GameObject spacebar;
    public GameObject active;
    public GameObject cooldown;
    public GameObject dummyPrefab;
    private GameObject dummyHolder;
    public Vector3 dummyLocation;
    private bool hasRightClicked;
    private bool hasLeftClicked;
    private bool hasPressedSpace;
    public bool disableTutorial;

    [Header("Background Colour")]
    public bool colourTransition;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint = 0;
    public float time;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found more than one Tutorial Manager instance in the scene.");
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        cam = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        DetectTutorialStage();

        if (colourTransition)
        {
            Transition();
        }

    }

    public void DetectTutorialStage()
    {
        if (!hasRightClicked && Input.GetKeyDown(KeyCode.Mouse1))
        {
            hasRightClicked = true;
            Invoke("FinishRightClickTutorial", 2f);
        }

        if (!hasLeftClicked && hasRightClicked && Input.GetKeyDown(KeyCode.Mouse0))
        {
            hasLeftClicked = true;
            Invoke("FinishLeftClickTutorial", 2f);
        }

        if (!hasPressedSpace && hasLeftClicked && hasRightClicked && Input.GetKeyDown(KeyCode.Space))
        {
            FinishPressSpaceTutorial();
        }
    }

    public void FinishRightClickTutorial()
    {
        rightClick.SetActive(false);
        leftClick.SetActive(true);
    }

    public void FinishLeftClickTutorial()
    {
        leftClick.SetActive(false);
        cooldown.SetActive(false);
        spacebar.SetActive(true);
    }

    public void FinishPressSpaceTutorial()
    {
        if (!disableTutorial)
        {
            dummyHolder = Instantiate(dummyPrefab, dummyLocation, Quaternion.identity);
            hasPressedSpace = true;
            spacebar.SetActive(false);
            active.SetActive(true);
            Invoke("FinishAttackTutorial", 5f);
        }
    }

    public void FinishAttackTutorial()
    {
        if (!disableTutorial)
        {
            Destroy(dummyHolder);
            active.SetActive(false);
            cooldown.SetActive(true);
            Invoke("FinishLeftClickTutorial", 3f);
            hasPressedSpace = false;
        }
    }

    public void FinishTutorial()
    {
        disableTutorial = true;
        colourTransition = true;
        cooldown.SetActive(false);
        active.SetActive(false);
        GameObject.FindWithTag("MainMenuManager").GetComponent<MainMenuManager>().SpawnMainMenu();
    }

    public void DisableTutorial()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        cam = mainCamera.GetComponent<Camera>();
        rightClick.SetActive(false);
        leftClick.SetActive(false);
        spacebar.SetActive(false);
        active.SetActive(false);
        cooldown.SetActive(false);
        cam.backgroundColor = backgroundColours[1];
        GameObject.FindWithTag("MainMenuManager").GetComponent<MainMenuManager>().SpawnMainMenu();
    }

    void Transition()
    {
        targetPoint += Time.deltaTime / time;
        cam.backgroundColor = Color.Lerp(backgroundColours[currentColorIndex], backgroundColours[targetColorIndex], targetPoint);

        if (targetPoint >= 1f)
        {
            colourTransition = false;
        }
    }

    public void CheckTutorialStatus()
    {
        if (disableTutorial)
        {
            DisableTutorial();
        }

        else
        {
            return;
        }
    }
}
