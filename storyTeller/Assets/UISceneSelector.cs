using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneSelector : MonoBehaviour {

	public GameObject panel;
	public SceneButton sceneButton;
	public Transform container;
	bool isNewScene;
	public UIAllScenesMenu uIAllScenesMenu;

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
		print ("isNewScene " + isNewScene + " id: " + sceneID + "   backgroundID " + backgroundID);
		if (isNewScene) {
			print ("Events.AddNewScene (sceneID, backgroundID);");
			uIAllScenesMenu.AddNewScene (sceneID, backgroundID);
		} else {
			print ("Events.OnChangeBackground (backgroundID);");
			Events.OnChangeBackground (backgroundID);
		}
		panel.SetActive (false);
	}
}
