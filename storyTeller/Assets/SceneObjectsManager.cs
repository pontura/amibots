using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour {

	public List<SceneObject> all;
	public UIDragItem uiDragItem;

	void Start () {
		Events.AddGenericObject += AddGenericObject;
        Events.ClickedOnSceneObject += ClickedOnSceneObject;
    //	Events.ClickedOn += ClickedOn;
    }
    void ClickedOnSceneObject(SceneObject so)
    {
        print("ClickedOnSceneObject " + uiDragItem.isDragging);
        if (uiDragItem.isDragging) return;
        if (World.Instance.worldStates.state == WorldStates.states.SCENE_EDITOR)
        {
            Vector3 pos = so.transform.localPosition;
            
            Tile tile = World.Instance.scenesManager.sceneActive.tiles.GetTileByPosition(pos);

            if (tile != null)
                Events.Blocktile(tile, false);

            Events.OnDrag(so.data);
            all.Remove(so);
            Destroy(so.gameObject);
           
        }
    }
    SceneObject draggableSceneObject;
    public void AddGenericObjectToDrag(SceneObjectData data)
    {
        GenericObject go = GetGenericObjectByName(data.sceneObjectName);
        draggableSceneObject = Instantiate(go);
        draggableSceneObject.data = data;
        draggableSceneObject.transform.SetParent(World.Instance.scenesManager.sceneActive.sceneObjects);
        draggableSceneObject.Init(data, Vector3.zero);
        draggableSceneObject.transform.localEulerAngles = new Vector3(90 + 20, 0, 0);
        draggableSceneObject.transform.localPosition = new Vector3(1000, 0, 0);
        go.OnSetColliders(false);
    }
    public void UpdatePosition(Vector3 pos)
    {
        if (draggableSceneObject == null) return;
        draggableSceneObject.transform.localPosition = pos;
    }
    public void SetActiveDraggableItem(bool isActive)
    {
        draggableSceneObject.gameObject.SetActive(isActive);
    }
    public void DestroyDraggableItem()
    {
        if (draggableSceneObject == null) return;
        Destroy(draggableSceneObject.gameObject);
        draggableSceneObject = null;
    }
    void AddGenericObject (SceneObjectData data, Vector2 pos) {
		GenericObject go = GetGenericObjectByName (data.sceneObjectName);
		SceneObject newGenericObject = Instantiate ( go );
        newGenericObject.data = data;
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
