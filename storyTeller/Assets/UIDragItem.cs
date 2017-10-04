using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragItem : MonoBehaviour {

	public GameObject dragItem;
	public bool isDragging;

	void Start () {
		SetActive (false);
		Events.OnDrag += OnDrag;
		Events.OnEndDrag += OnEndDrag;
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
			Invoke ("Reset", 0.05f);
		}
	}
	void Reset()
	{
		OnEndDrag ();
	}
	void OnDrag(string soName)
	{
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
