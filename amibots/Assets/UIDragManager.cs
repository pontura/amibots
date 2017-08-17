using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragManager : MonoBehaviour {

	UiClassManager uiClassManager;
	bool isOver;
	string lastClassSelected;

	void Start () {
		uiClassManager = GetComponent<UiClassManager> ();
		Events.IsOverFunctionSlot += IsOverFunctionSlot;
		Events.DragStart += DragStart;
		Events.DragEnd += DragEnd;
	}
	void DragStart(string className)
	{
		this.lastClassSelected = className;
	}
	void IsOverFunctionSlot(bool _isOver)
	{
		isOver = _isOver;
	}
	void DragEnd()
	{
		if (isOver) {
			AmiClass ac = Data.Instance.amiClasses.GetClassesByClassName (lastClassSelected);
			uiClassManager.AddFunction (ac);
		} else
			lastClassSelected = "";
	}


}
