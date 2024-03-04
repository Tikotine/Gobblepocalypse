using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<EventInstance> eventInstances;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }

        instance = this;

        eventInstances = new List<EventInstance>();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    { 
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    { 
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);    //Create an event instance based on the event reference passed into the method
        eventInstances.Add(eventInstance);      //Every time an instance is created, add it to the list

        return eventInstance;
    }

    private void CleanUp()
    { 
        foreach(EventInstance e in eventInstances)      //Stops all instances in the list
        {
            e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            e.release();
        }
    }

    private void OnDestroy()    //On scene change etc
    {
        CleanUp();
    }
}
