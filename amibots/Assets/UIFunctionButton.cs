using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionButton : MonoBehaviour {

    public string className;
    public Text field;
    public Image image;

	public void Init(string className) {
        this.className = className;
        field.text = className;
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
