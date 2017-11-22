using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDragItem : MonoBehaviour {

    public Image image;
	public SceneObjectData sceneObjectData;
	public GameObject dragItem;
	public bool isDragging;

	void Start () {
		SetActive (false);
		Events.OnDrag += OnDrag;
		//Events.OnEndDrag += OnEndDrag;
		Events.ClickedOn += ClickedOn;
	}
	void Update()
	{
        if (isDragging)
        {
            dragItem.transform.position = Input.mousePosition;

            RaycastHit hit;
            Ray ray = World.Instance.scenesManager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
                {
                    World.Instance.sceneObjectsManager.UpdatePosition(hit.transform.position);
                }
            }
                
            
           
            if (dragItem.transform.position.x < 100 || dragItem.transform.position.y < 20)
            {
                OnEndDrag();
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
            OnEndDrag ();
		}
	}
	void OnDrag(SceneObjectData sceneObjectData)
	{
        World.Instance.sceneObjectsManager.AddGenericObjectToDrag(sceneObjectData);
        this.sceneObjectData = sceneObjectData;
		isDragging = true;
		SetActive (true);
	}
	void OnEndDrag()
	{
		isDragging = false;
		SetActive (false);
	}
	void SetActive (bool isActive) {
		dragItem.SetActive (isActive);
	}
}
