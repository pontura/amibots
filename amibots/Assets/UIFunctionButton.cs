using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionButton : MonoBehaviour {

    public string className;

	void Start () {
		
	}
    public void PointerDown()
    {
		Events.DragStart (className);
    }
    public void PointerUp()
    {
		Events.DragEnd ();
    }
}
