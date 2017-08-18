using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragItem : MonoBehaviour {

	public UIIcons _icons;
	private UIIcons icons;
	bool isOn;

	void Start()
	{
		DragEnd ();
		Events.DragStart += DragStart;
		Events.DragEnd += DragEnd;
		Events.DragStartGameObject += DragStartGameObject;
		icons = Instantiate (_icons);
		icons.transform.SetParent (transform);
		icons.transform.localPosition = Vector3.zero;
	}
	void Update()
	{
		if (isOn) {
			transform.position = Input.mousePosition;
		}
	}
	public void DragStart(string className)
	{
		//icons.transform.localPosition = new Vector3 (0, 0, 0);
		isOn = true;
		icons.Init (className);
	}
	void DragEnd()
	{
		isOn = false;
		transform.localPosition = new Vector3 (1000, 0, 0);
	}
	void DragStartGameObject(GameObject go)
	{
		//icons.transform.localPosition = new Vector3 (1000, 0, 0);
		isOn = true;
		go.transform.SetParent (icons.transform);
		go.transform.localPosition =new Vector3(240,-20,0);
	}
}
