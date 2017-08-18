using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragManager : MonoBehaviour {

	UiClassManager uiClassManager;
	bool isOverSlot;
	string lastClassSelected;
	GameObject draggedGO;

	void Start () {
		uiClassManager = GetComponent<UiClassManager> ();
		Events.IsOverFunctionSlot += IsOverFunctionSlot;
		Events.DragStart += DragStart;
		Events.DragStartGameObject += DragStartGameObject;
		Events.DragEnd += DragEnd;
	}
	void DragStart(string className)
	{
		this.lastClassSelected = className;
	}
	void DragStartGameObject(GameObject go)
	{
		this.draggedGO = go;
	}
	void IsOverFunctionSlot(bool _isOver)
	{
		isOverSlot = _isOver;
	}
	void DragEnd()
	{
		if (isOverSlot && lastClassSelected != "") {
			AmiClass ac = Data.Instance.amiClasses.GetClassesByClassName (lastClassSelected);
			uiClassManager.AddFunction (ac);
		} else if (isOverSlot && draggedGO) {
			uiClassManager.RepositionateFunction (draggedGO);
		} else if (!isOverSlot && draggedGO != null) {
			Destroy (draggedGO);
		}

		draggedGO = null;
		lastClassSelected = "";

	}


}
