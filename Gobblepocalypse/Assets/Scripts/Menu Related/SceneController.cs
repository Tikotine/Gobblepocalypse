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

    private void Awake()
    {
        transition = GetComponentInChildren<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void DoTransition(string sceneName, MusicControl mc)
    {
        StartCoroutine(Do(sceneName, mc));
    }

    IEnumerator Do(string sceneName, MusicControl mc)
    {
        transition.SetTrigger("Start");
        Scene s = SceneManager.GetActiveScene();
        lastSceneBuildIndex = s.buildIndex;
        yield return new WaitForSeconds(transitionTime);
        AudioManager.SetMusicControl(mc);
        SceneManager.LoadScene(sceneName);
    }

    public void DoTransitionInt(int sceneIndex, MusicControl mc)
    {
        StartCoroutine(DoInt(sceneIndex, mc));
    }

    IEnumerator DoInt(int sceneIndex, MusicControl mc)
    {
        transition.SetTrigger("Start");
        Scene s = SceneManager.GetActiveScene();
        lastSceneBuildIndex = s.buildIndex;
        yield return new WaitForSeconds(transitionTime);
        AudioManager.SetMusicControl(mc);
        SceneManager.LoadScene(sceneIndex);
    }
}
