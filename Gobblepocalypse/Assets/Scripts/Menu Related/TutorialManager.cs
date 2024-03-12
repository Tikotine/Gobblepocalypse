using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject cooldown;
    public bool hasRightClicked;
    public bool hasLeftClicked;
    public bool hasPressedSpace;
    public bool hasFinishedCooldown;
    public bool disableTutorial;

    [Header("Background Colour")]
    public bool colourTransition;
    private int currentColorIndex = 0;
    private int targetColorIndex = 1;
    private float targetPoint;
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
            FinishRightClickTutorial();
        }

        if (!hasLeftClicked && hasRightClicked && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FinishLeftClickTutorial();
        }

        if (!hasPressedSpace && hasLeftClicked && hasRightClicked && Input.GetKeyDown(KeyCode.Space))
        {
            FinishPressSpaceTutorial();
        }
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
        spacebar.SetActive(true);
    }

    public void FinishPressSpaceTutorial()
    { 
        hasPressedSpace = true;
        spacebar.SetActive(false);
        cooldown.SetActive(true);
        Invoke("FinishTutorial", 5f);
    }

    public void FinishTutorial()
    {
        colourTransition = true;
        GameObject.FindWithTag("MainMenuManager").GetComponent<MainMenuManager>().SpawnMainMenu();
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

    public void DisableTutorial()
    {
        rightClick.SetActive(false);
        leftClick.SetActive(false);
        spacebar.SetActive(false);
        cam.backgroundColor = backgroundColours[1];
        GameObject.FindWithTag("MainMenuManager").GetComponent<MainMenuManager>().SpawnMainMenu();
    }

    void Transition()
    {
        targetPoint += Time.deltaTime / time;
        cam.backgroundColor = Color.Lerp(backgroundColours[currentColorIndex], backgroundColours[targetColorIndex], targetPoint);


        if (targetPoint >= 1f)
        {
            targetPoint = 0f;
            currentColorIndex = targetColorIndex;
            targetColorIndex++;

            if (targetColorIndex == backgroundColours.Length)
            {
                targetColorIndex = 0;
            }
        }

    }
}
