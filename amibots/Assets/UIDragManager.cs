using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragManager : MonoBehaviour {

   public UIFunctionSlot parent_Slot;

    UiClassManager uiClassManager;
    UIFunctionSlot overSlot;
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
		print ("DragStartGameObject " + go);
		this.draggedGO = go;
	}
	void IsOverFunctionSlot(UIFunctionSlot _overSlot)
	{
        if(overSlot != null && overSlot.gameObject.name == "FunctionSlot_Childs")
            overSlot = parent_Slot;
        else
            overSlot = _overSlot;
	}
    
	void DragEnd()
	{
		if (overSlot != null && lastClassSelected != "") {
			AmiClass ac = Data.Instance.amiClasses.GetClassesByClassName (lastClassSelected);
			uiClassManager.AddFunction (ac, overSlot.container.transform);
		} else if (overSlot != null && draggedGO) {
			uiClassManager.RepositionateFunction (overSlot, draggedGO);
		} else if (overSlot == null && draggedGO != null) {
			Destroy (draggedGO);
		}

		draggedGO = null;
		lastClassSelected = "";

	}


}
