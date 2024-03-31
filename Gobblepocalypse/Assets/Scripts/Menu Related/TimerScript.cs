using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    private SceneController sc;
    private TextMeshProUGUI TimerTxt;
    private float time;
    private bool end = false;
    private string timeString;

    private void Start()
    {
        sc = FindObjectOfType<SceneController>();
        TimerTxt = GetComponent<TextMeshProUGUI>();
        time = 0;
    }

    private void Update()
    {
        if (!end)
        {
            time += Time.deltaTime;
            updateTimer(time);
        }
    }

    private void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerTxt.text = timeString;
    }

    public void endTimer()
    {
        end = true;

        //check what lvl
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //check if record exists
            //check if more than prev record, show in lvl complete n update in sc
            case 1:
                if(sc.Lvl1Best == 0)
                {
                    Debug.Log("no prev record");
                    sc.Lvl1time = time;
                    sc.Lvl1Best = time;
                    sc.NewBest = true;
                }
                else
                {
                    Debug.Log("updating record");
                    if (sc.Lvl1time > time)
                    {
                        sc.Lvl1time = time;
                        sc.Lvl1Best = time;
                        //new highscore
                        sc.NewBest = true;
                    }
                    else
                    {
                        sc.Lvl1time = time;
                        //best no change.
                        sc.NewBest = false;
                    }
                }
                break;

            case 2:
                if (sc.Lvl2Best == 0)
                {
                    Debug.Log("no prev record");
                    sc.Lvl2time = time;
                    sc.Lvl2Best = time;
                    sc.NewBest = true;
                }
                else
                {
                    Debug.Log("updating record");
                    if (sc.Lvl2time > time)
                    {
                        sc.Lvl2time = time;
                        sc.Lvl2Best = time;
                        //new highscore
                        sc.NewBest = true;
                    }
                    else
                    {
                        sc.Lvl2time = time;
                        //best no change.
                        sc.NewBest = false;
                    }
                }
                break;

            case 3:
                if (sc.Lvl3Best == 0)
                {
                    Debug.Log("no prev record");
                    sc.Lvl3time = time;
                    sc.Lvl3Best = time;
                    sc.NewBest = true;
                }
                else
                {
                    Debug.Log("updating record");
                    if (sc.Lvl3time > time)
                    {
                        sc.Lvl3time = time;
                        sc.Lvl3Best = time;
                        //new highscore
                        sc.NewBest = true;
                    }
                    else
                    {
                        sc.Lvl3time = time;
                        //best no change.
                        sc.NewBest = false;
                    }
                }
                break;
        }
    }
}