using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneView : MonoBehaviour {

	public SpriteRenderer background;

	public GameObject panel;
	void Start () {
		Events.OnChangeBackground += OnChangeBackground;
		panel.SetActive (true);
	}
	void OnChangeBackground(int id)
	{
		World.Instance.scenesManager.sceneActive.ChangeBackground (id);
	}

}
