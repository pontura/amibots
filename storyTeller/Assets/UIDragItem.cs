﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragItem : MonoBehaviour {

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
			dragItem.transform.position = Input.mousePosition;
	}
	void ClickedOn(Tile tile)
	{
		if (isDragging) {
			Events.AddGenericObject(sceneObjectData, new Vector2((int)tile.transform.position.x, (int)tile.transform.position.z));
			Events.Blocktile (tile, true);
			OnEndDrag ();
		}
	}
	void OnDrag(SceneObjectData sceneObjectData)
	{
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