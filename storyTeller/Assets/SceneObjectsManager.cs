using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour {

	public Transform container;
	public SceneObject[] all;
	public UIDragItem uiDragItem;

	void Start () {
		Events.AddGenericObject += AddGenericObject;
		Events.ClickedOn += ClickedOn;
	}
	void ClickedOn(Tile tile)
	{
		if (!uiDragItem.isDragging)
			return;
		SceneObject newGenericObject = Instantiate ( GetGenericObject() );
		newGenericObject.transform.SetParent (container);
		newGenericObject.Init ( tile.GetVector2() );
		Events.Blocktile (tile, true);
	}
	void AddGenericObject (Vector2 pos) {
		SceneObject newGenericObject = Instantiate ( GetGenericObject() );
		newGenericObject.transform.SetParent (container);
		newGenericObject.Init (pos);
	}
	GenericObject GetGenericObject()
	{
		foreach (SceneObject so in all) {
			GenericObject go = so.GetComponent<GenericObject> ();
			if (go)
				return go;
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
