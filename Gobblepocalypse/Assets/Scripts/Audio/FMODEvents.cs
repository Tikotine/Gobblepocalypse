using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience1 { get; private set; }
    [field: SerializeField] public EventReference ambience2 { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference BGMControl { get; private set; }

    [field: Header("Button SFX")]
    [field: SerializeField] public EventReference buttonPress { get; private set; }
    [field: SerializeField] public EventReference buttonHover { get; private set; }

    [field: Header("Transition SFX")]
    [field: SerializeField] public EventReference transitionSound { get; private set; }
    [field: SerializeField] public EventReference transitionOutSound { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerRoll { get; private set; }
    [field: SerializeField] public EventReference playerCharge { get; private set; }
    [field: SerializeField] public EventReference playerGainCharge { get; private set; }
    [field: SerializeField] public EventReference playerShoot { get; private set; }
    [field: SerializeField] public EventReference playerAttackModeActivate { get; private set; }
    [field: SerializeField] public EventReference playerAttackModeCooldown { get; private set; }
    [field: SerializeField] public EventReference playerAttackModeReady { get; private set; }
    [field: SerializeField] public EventReference playerBossInterrupt { get; private set; }
    [field: SerializeField] public EventReference playerWallBounce { get; private set; }
    [field: SerializeField] public EventReference playerWallSplat { get; private set; }
    [field: SerializeField] public EventReference playerWallStick { get; private set; }
    [field: SerializeField] public EventReference playerSpiked { get; private set; }
    [field: SerializeField] public EventReference playerDeath { get; private set; }

    [field: Header("Boss SFX")]
    [field: SerializeField] public EventReference bossMovement { get; private set; }
    [field: SerializeField] public EventReference bossCharge { get; private set; }
    [field: SerializeField] public EventReference bossAttack { get; private set; }
    [field: SerializeField] public EventReference bossInterrupted { get; private set; }

    [field: Header("Collectable SFX")]
    [field: SerializeField] public EventReference collectableCollected { get; private set; }
    [field: SerializeField] public EventReference collectableIdle { get; private set; }

    [field: Header("Checkpoint SFX")]
    [field: SerializeField] public EventReference checkpointActivated { get; private set; }

    [field: Header("Star SFX")]
    [field: SerializeField] public EventReference starCollected { get; private set; }
    [field: SerializeField] public EventReference starIdle { get; private set; }

    [field: Header("Level Complete SFX")]
    [field: SerializeField] public EventReference levelComplete { get; private set; }


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
