using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public SceneObjectsManager sceneObjectsManager;
	[HideInInspector]
	public TimeLine timeLine;
	[HideInInspector]
	public CharactersManager charactersManager;
	public ScenesManager scenesManager;
    static World mInstance = null;

    public static World Instance
    {
        get
        {
            return mInstance;
        }
    }
    void Awake()
    {
      	mInstance = this;
		sceneObjectsManager = GetComponent<SceneObjectsManager> ();
		timeLine = GetComponent<TimeLine> ();
		charactersManager =  GetComponent<CharactersManager> ();
		scenesManager = GetComponent<ScenesManager> ();
    }
}
