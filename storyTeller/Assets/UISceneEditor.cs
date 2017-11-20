using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneEditor : MonoBehaviour {

	public Transform container;
	public UIButton uiButton;
	public GameObject panel;
	public UIButton sceneEditorButton;
	SceneObject.types selectedType;

	void Start () {
		SetActive (false);
		Events.OnUIButtonClicked += OnUIButtonClicked;
	}
	void OnUIButtonClicked(UIButton _uiButton)
	{
		switch (_uiButton.type)
		{
		case UIButton.types.SCENEOBJECT_MENU:
                World.Instance.worldStates.state = WorldStates.states.SCENE_EDITOR;
			SetActive (true);
			break;
		case UIButton.types.CHARACTER_EDITOR:
                World.Instance.worldStates.state = WorldStates.states.CHARACTERS_EDITOR;
                SetActive (false);
				break;
		case UIButton.types.SCENEOBJECT:
                
			SceneObjectData data = new SceneObjectData ();
			data.sceneObjectName = _uiButton.field.text;
			data.type = selectedType;
			Events.OnDrag (data);
                print(data.sceneObjectName);
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
	public void OpenByID(int id)
	{
		if (id == 1)
			GetComponent<UISceneSelector> ().Open (false);
			//Open (SceneObject.types.BACKGROUND);
		else if (id == 2)
			Open (SceneObject.types.FURNITURES);
		else if (id == 3)
			Open (SceneObject.types.VEHICLES);
		else if (id == 4)
			Open (SceneObject.types.SPORT);
	}

	void Open(SceneObject.types _type)
	{
		this.selectedType = _type;
		Utils.RemoveAllChildsIn(container);
		int id = 0;
		foreach (SceneObject so in World.Instance.sceneObjectsManager.GetObjectsByType(_type))
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
	public void Backgrounds()
	{

	}
	public void Furtnitures()
	{

	}

}
