using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInScene : MonoBehaviour {

    public float smooth;
    public Vector3 offsetPos;
    private Camera camera_in_scene;
    public CharactersManager charactersManager;

	void Start () {
        camera_in_scene = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (charactersManager.selectedCharacter != null)
        {
            Vector3 pos = charactersManager.selectedCharacter.transform.localPosition;
            pos += offsetPos;
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, smooth);
        }
    }
}
