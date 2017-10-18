using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInScene : MonoBehaviour {

    public float smooth;
    public Vector3 offsetPos;
    private Camera camera_in_scene;
    public CharactersManager charactersManager;
	private Vector2 limitsY = new Vector2(-12, -3);
	private Vector2 limitsX = new Vector2(4, 21);

	void Start () {
        camera_in_scene = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void _________Update () {
        if (charactersManager.selectedCharacter != null)
        {
            Vector3 pos = charactersManager.selectedCharacter.transform.localPosition;
            pos += offsetPos;

			if (pos.z < limitsY.x)
				pos.z = limitsY.x;
			else if (pos.z > limitsY.y)
				pos.z = limitsY.y;

			if (pos.x < limitsX.x)
				pos.x = limitsX.x;
			else if (pos.x > limitsX.y)
				pos.x = limitsX.y;
			
            transform.localPosition = Vector3.Lerp(transform.localPosition, pos, smooth);
        }
    }
}
