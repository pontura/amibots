﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneSelector : MonoBehaviour {

	public GameObject panel;
	public SceneButton sceneButton;
	public Transform container;
	bool isNewScene;
	public UIAllScenesMenu uIAllScenesMenu;
    public Text field;

	public void Open (bool isNewScene) {
        print("_______________________");
        if(isNewScene)
        {
            field.text = "Where do you want to start your film?";
        }
        else
        {
            field.text = "Select a new location";
        }
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
    public void AddTitleScreen()
    {
        GetComponent<UIScreenTitle>().Open();
        panel.SetActive(false);
    }
    public void SetSelected(int sceneID, int backgroundID)
	{
		if (isNewScene) {
			uIAllScenesMenu.AddNewScene (sceneID, backgroundID);
            GetComponent<UiCustomizer>().CreateNew(false);
        } else {
			Events.OnChangeBackground (backgroundID);
		}
		panel.SetActive (false);
	}
}
