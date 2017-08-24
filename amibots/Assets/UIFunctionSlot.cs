using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionSlot : MonoBehaviour {

    public GameObject container;

    void Start()
    {
        Events.DragEnd += DragEnd;
    }
    void OnDestroy()
    {
        Events.DragEnd -= DragEnd;
    }
    void DragEnd()
    {
        Invoke("RecalculateSize", 0.1f);
    }
    void RecalculateSize()
    {
       Vector2 s= GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = s;
    }

	public void OnOver () {
		Events.IsOverFunctionSlot (this);
	}
    public void OnExit()
    {
        Events.IsOverFunctionSlot (null);
    }

}
