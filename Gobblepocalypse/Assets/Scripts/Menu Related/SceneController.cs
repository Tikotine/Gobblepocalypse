using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//jme
public class SceneController : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public int starAmt;
    public int lastSceneBuildIndex;
    private static SceneController Instance;
    private GameObject player;

    private void Awake()
    {
        transition = GetComponentInChildren<Animator>();
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void DoTransition(string sceneName, MusicControl mc)
    {
        StartCoroutine(Do(sceneName, mc));
    }

    IEnumerator Do(string sceneName, MusicControl mc)
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.transitionSound, player.transform.position);
        }

        else 
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.transitionSound, transform.position);
        }

        transition.SetTrigger("Start");
        Scene s = SceneManager.GetActiveScene();
        lastSceneBuildIndex = s.buildIndex;
        yield return new WaitForSeconds(transitionTime);

        if (player != null)
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.transitionOutSound, player.transform.position);
        }

        else 
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.transitionOutSound, transform.position);
        }
        AudioManager.SetMusicControl(mc);
        SceneManager.LoadScene(sceneName);
    }

    public void DoTransitionInt(int sceneIndex, MusicControl mc)
    {
        StartCoroutine(DoInt(sceneIndex, mc));
    }

    IEnumerator DoInt(int sceneIndex, MusicControl mc)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.transitionSound, transform.position);
        transition.SetTrigger("Start");
        Scene s = SceneManager.GetActiveScene();
        lastSceneBuildIndex = s.buildIndex;
        yield return new WaitForSeconds(transitionTime);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.transitionOutSound, transform.position);
        AudioManager.SetMusicControl(mc);
        SceneManager.LoadScene(sceneIndex);
    }
}
