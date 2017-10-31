using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour {

	public Image image;
	UISceneSelector sceneSelector;
	UIAllScenesMenu allSceneMenu;
	public int id;
	int backgroundID;

	public void InitInMenu(UIAllScenesMenu allSceneMenu, int _id, int backgroundID)
	{		
		this.id = _id;
		this.backgroundID = backgroundID;
		this.allSceneMenu = allSceneMenu;
		image.sprite = Resources.Load("scenes/" + backgroundID, typeof(Sprite)) as Sprite;
	}
	public void Init(UISceneSelector sceneSelector,  int _id, int backgroundID)
	{
		this.id = _id;
		this.backgroundID = backgroundID;
		this.sceneSelector = sceneSelector;
		image.sprite = Resources.Load("scenes/" + backgroundID, typeof(Sprite)) as Sprite;
	}
	public void Clicked()
	{
		print ("Clicked " + id);
		if (allSceneMenu != null)
			allSceneMenu.SetActive (id);
		else
			sceneSelector.SetSelected (id, backgroundID);
	}
}
