using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAllScenesMenu : MonoBehaviour {

	public SceneButton sceneButton;
	public Transform container;

	SceneButton activeSceneButton;
	public int activeSceneID;

	void Start () {
		Events.OnChangeBackground += OnChangeBackground;
		Add ();
	}	
	public void SetActive(int id)
	{
		Events.OnActivateScene (id);
	}
	void OnChangeBackground(int id)
	{
		activeSceneButton.InitInMenu (this, activeSceneID-1, id);
	}
	public void Add()
	{
		GetComponent<UISceneSelector> ().Open (true);
		activeSceneID++;
		SceneButton newSceneButton = Instantiate (sceneButton);
		newSceneButton.transform.SetParent (container);
		newSceneButton.transform.localScale = Vector2.one;
		print (activeSceneID);
		newSceneButton.InitInMenu (this, activeSceneID, activeSceneID);
		activeSceneButton = newSceneButton;
	}
}
