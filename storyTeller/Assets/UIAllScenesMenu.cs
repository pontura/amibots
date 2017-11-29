using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAllScenesMenu : MonoBehaviour {

	public SceneButton sceneButton;
	public Transform container;

	SceneButton activeSceneButton;
	public int activeSceneID;

	void Start () {
		//Events.OnChangeBackground += OnChangeBackground;
		//Events.AddNewScene += AddNewScene;
	}	
	public void SetActive(int id)
	{
		Events.OnActivateScene (id);
	}
	void OnChangeBackground(int id)
	{
		//activeSceneButton.UpdateThumbButton(activeSceneID);
	}
	public void AddNewScene(int sceneID, int backgroundID)
	{
        GetComponent<UISceneSelector> ().Open (true);
		activeSceneID++;
		SceneButton newSceneButton = Instantiate (sceneButton);
		newSceneButton.transform.SetParent (container);
		newSceneButton.transform.localScale = Vector2.one;
		print (activeSceneID);
		newSceneButton.InitInMenu (this, sceneID);
		activeSceneButton = newSceneButton;

		Events.AddNewScene (sceneID, backgroundID);
	}
}
