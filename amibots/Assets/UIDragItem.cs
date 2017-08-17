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
		isOn = true;
		icons.Init (className);
	}
	void DragEnd()
	{
		isOn = false;
		transform.localPosition = new Vector3 (1000, 0, 0);
	}
}
