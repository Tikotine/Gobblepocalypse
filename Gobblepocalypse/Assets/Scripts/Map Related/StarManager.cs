using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public static StarManager instance { get; private set; }

    [Header("Menu Stars")]
    public GameObject[] menuStarsPrefabs;
    public GameObject[] menuStarsWaypoints;
    public Vector3[] menuStarsLocations;
    public bool[] starsCollected;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found more than one Starmanager instance in the scene.");
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < menuStarsWaypoints.Length; i++)
        {
            menuStarsLocations[i] = menuStarsWaypoints[i].transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStars()
    {
        for (int i = 0; i < starsCollected.Length; i++)     //For every boolean representing a star
        {
            if (starsCollected[i] == true)      //If true
            {
              GameObject star = Instantiate(menuStarsPrefabs[i], menuStarsLocations[i], Quaternion.identity);       //Instantiate a star at the location
              menuStarsWaypoints[i] = star;
            }
        }
    }

    public void RemoveStars()
    {
        foreach (GameObject e in menuStarsWaypoints)
        {
            Destroy(e);
        }
    }

}
