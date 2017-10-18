using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	public SceneObjectsManager sceneObjectsManager;
    public CameraInScene camera_in_scene;
	[HideInInspector]
	public Tiles tiles;
	[HideInInspector]
	public TimeLine timeLine;
	[HideInInspector]
	public CharactersManager charactersManager;
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
		tiles = GetComponent<Tiles> ();
		sceneObjectsManager = GetComponent<SceneObjectsManager> ();
		timeLine = GetComponent<TimeLine> ();
		charactersManager =  GetComponent<CharactersManager> ();
    }
}
