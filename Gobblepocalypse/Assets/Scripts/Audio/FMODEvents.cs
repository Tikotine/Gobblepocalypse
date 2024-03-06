using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience1 { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference BGMControl { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerRoll { get; private set; }

    [field: Header("Collectable SFX")]
    [field: SerializeField] public EventReference collectableCollected { get; private set; }
    [field: SerializeField] public EventReference collectableIdle { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found more than one FMODEvents instance in the scene.");
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        else 
        {
            Destroy(gameObject);
        }
    }
}
