using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }

        instance = this;

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience1);      //Play ambience on start
        InitializeMusic(FMODEvents.instance.BGM1);      //Play BGM on start
    }

    private void InitializeMusic(EventReference musicEventReference)
    { 
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    { 
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);    //Initialize by calling the create function
        ambienceEventInstance.start();      
    }

    //Call this method in another script to change the ambience intensity
    public void SetAmbienceParameter(string parameterName, float parameterValue)       //A method to set the parameter value. Intakes the parameter name and value to set
    { 
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);        //Set the parameter value based on input
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

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)   //The event reference we want to override it with & the gameobject the emitter is attached to
    { 
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;        //Override the eventreference with the one passed into this method
        eventEmitters.Add(emitter);     //Add the emitter to the list for cleanup
        return emitter;
    }

    private void CleanUp()
    { 
        foreach(EventInstance e in eventInstances)      //Stops all instances in the list
        {
            e.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            e.release();
        }

        foreach (StudioEventEmitter e in eventEmitters)        //Stops all emitters in the list
        { 
            e.Stop();
        }
    }

    private void OnDestroy()    //On scene change etc
    {
        CleanUp();
    }
}
