using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionButton : MonoBehaviour {

    public string className;

	void Start () {
		
	}
    public void PointerDown()
    {
        transform.localScale = Vector3.zero;
		Events.DragStart (className);
    }
    public void PointerUp()
    {
        transform.localScale = Vector3.one;
        Events.DragEnd ();
    }
}
