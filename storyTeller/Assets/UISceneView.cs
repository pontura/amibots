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
	int id;
	void OnChangeBackground(int id)
	{
		this.id = id;
		World.Instance.scenesManager.sceneActive.ChangeBackground (id);
	}

}
