using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDragItem : MonoBehaviour {

    public Image image;
	public SceneObjectData sceneObjectData;
	public GameObject dragItem;
	public bool isDragging;
    SceneObjectsManager sceneObjectsManager;

    void Start () {
        sceneObjectsManager = World.Instance.sceneObjectsManager;
        SetActive (false);
		Events.OnDrag += OnDrag;
		Events.OnEndDrag += OnEndDrag;
		Events.ClickedOn += ClickedOn;
	}
	void Update()
	{
        if (isDragging)
        {
            dragItem.transform.position = Input.mousePosition;

            RaycastHit hit;
            Ray ray = World.Instance.scenesManager.cam.ScreenPointToRay(Input.mousePosition);
            bool isOverTile = false;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
                    isOverTile = true;
            }
            if (isOverTile)
            {
                sceneObjectsManager.SetActiveDraggableItem(true);
                sceneObjectsManager.UpdatePosition(hit.transform.position);
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
                sceneObjectsManager.SetActiveDraggableItem(false);
            }
                



        if (dragItem.transform.position.x < 100 || dragItem.transform.position.y < 20)
            {
                Events.OnEndDrag();
            }
        }
	}
	void ClickedOn(Tile tile)
	{
		if (isDragging) {
			//if (sceneObjectData.type == SceneObject.types.BACKGROUND) {
			//	Events.OnChangeBackground (int.Parse(sceneObjectData.sceneObjectName));
			//} else {
				Events.AddGenericObject (sceneObjectData, new Vector2 ((int)tile.transform.position.x, (int)tile.transform.position.z));
				Events.Blocktile (tile, true);
            //}
		}
    }
	void OnDrag(SceneObjectData sceneObjectData)
	{
        if (isDragging) return;
        image.enabled = true;
        string url = "sceneObjects/" + sceneObjectData.sceneObjectName;
        print("drag: " + url);
        image.sprite = Resources.Load(url,  typeof(Sprite)) as Sprite;        
        Events.OnSetColliders(false);
        sceneObjectsManager.AddGenericObjectToDrag(sceneObjectData);
        this.sceneObjectData = sceneObjectData;
		isDragging = true;
		SetActive (true);
	}
	void OnEndDrag()
	{
        print("OnEndDrag " + isDragging);
        image.enabled = false;
        Events.OnSetColliders(true);
        sceneObjectsManager.DestroyDraggableItem();
		isDragging = false;
		SetActive (false);
	}
	void SetActive (bool isActive) {
		dragItem.SetActive (isActive);
	}
}
