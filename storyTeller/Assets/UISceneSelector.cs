using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneSelector : MonoBehaviour {

	public GameObject panel;
	public SceneButton sceneButton;
	public Transform container;
	bool isNewScene;

	void Start () {		
		Open (true);
	}
	public void Open (bool isNewScene) {
		this.isNewScene = isNewScene;
		panel.SetActive (true);
		Utils.RemoveAllChildsIn (container);
		for (int a = 0; a < Data.Instance.settings.totalScenes; a++) {
			SceneButton sb = Instantiate (sceneButton);
			sb.transform.SetParent (container);
			sb.transform.localScale = Vector2.one;
			int id = World.Instance.scenesManager.scenesIngame.Count;
			sb.Init (this, id, a+1);
		}
	}
	public void SetSelected(int sceneID, int backgroundID)
	{
		print ("isNewScene " + isNewScene + " id: " + sceneID);
		if (isNewScene)
			Events.AddNewScene (sceneID, backgroundID);
		else
			Events.OnChangeBackground (backgroundID);
		panel.SetActive (false);
	}
}
