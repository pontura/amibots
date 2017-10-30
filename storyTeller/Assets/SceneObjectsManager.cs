using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour {

	public SceneObject[] all;
	public UIDragItem uiDragItem;

	void Start () {
		Events.AddGenericObject += AddGenericObject;
	//	Events.ClickedOn += ClickedOn;
	}
	void AddGenericObject (SceneObjectData data, Vector2 pos) {
		print (data.sceneObjectName);
		GenericObject go = GetGenericObjectByName (data.sceneObjectName);
		SceneObject newGenericObject = Instantiate ( go );
		newGenericObject.transform.SetParent (World.Instance.scenesManager.sceneActive.sceneObjects);
		newGenericObject.Init (data, pos);
		newGenericObject.transform.localEulerAngles = new Vector3 (90+20, 0, 0);
	}
	GenericObject GetGenericObjectByName(string name)
	{
		foreach (SceneObject so in all) {
			foreach (Transform asset in so.GetComponentsInChildren<Transform>()) {
				if (asset && asset.name == name)
					return so.GetComponent<GenericObject>();
			}
		}
		return null;
	}
	public List<SceneObject> GetObjectsByType(SceneObject.types type)
	{
		List<SceneObject> list = new List<SceneObject>();
		foreach(SceneObject so in all)
		{
			if (so.type == type)
				list.Add (so);
		}
		return list;
	}
}
