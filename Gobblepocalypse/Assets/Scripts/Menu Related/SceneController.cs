using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//jme
public class SceneController : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public int starAmt;

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
        yield return new WaitForSeconds(transitionTime);
        AudioManager.SetMusicControl(mc);
        SceneManager.LoadScene(sceneName);
    }
}
