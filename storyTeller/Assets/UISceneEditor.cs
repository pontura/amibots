using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneEditor : MonoBehaviour {

	public Transform container;
	public UIButton uiButton;
	public GameObject panel;
	public UIButton sceneEditorButton;

	void Start () {
		SetActive (false);
		Events.OnUIButtonClicked += OnUIButtonClicked;
	}
	void OnUIButtonClicked(UIButton uiButton)
	{
		switch (uiButton.type)
		{
		case UIButton.types.SCENE_EDITOR:
				SetActive (true);
				Open (SceneObject.types.FURNITURES);
				break;
		case UIButton.types.CHARACTER_EDITOR:
			SetActive (false);
				break;
		case UIButton.types.SCENEOBJECT:
			SceneObjectData data = new SceneObjectData ();
			data.sceneObjectName = uiButton.field.text;
			Events.OnDrag (data);
			break;
		}
	}
	void SetActive(bool isActive)
	{
		if (!isActive) {
			sceneEditorButton.gameObject.SetActive (true);
			panel.SetActive (false);
		} else {
			sceneEditorButton.gameObject.SetActive (false);
			panel.SetActive (true);
		}
	}
	public void Open(SceneObject.types type)
	{
		Utils.RemoveAllChildsIn(container);

		int id = 0;
		foreach (SceneObject so in World.Instance.sceneObjectsManager.GetObjectsByType(type))
		{
			foreach (GameObject go in so.GetComponent<GenericObject>().all)
			{
				UIButton newUiButton = Instantiate(uiButton);
				newUiButton.transform.SetParent(container);
				newUiButton.Init(id, go.name);
				newUiButton.type = UIButton.types.SCENEOBJECT;
				id++;
			}
		}
		panel.SetActive(true);
	}
	void Reset()
	{
		panel.SetActive(false);
	}

}
