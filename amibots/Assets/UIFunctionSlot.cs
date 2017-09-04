using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionSlot : MonoBehaviour {

    public GameObject container;
    public Image image;
    Color imageColor;
    public UIFunctionLine functionLine;

    void Start()
    {
        Events.DragEnd += DragEnd;
        imageColor = image.color;
    }
    void OnDestroy()
    {
        Events.DragEnd -= DragEnd;
    }
    void DragEnd()
    {
        container.SetActive(false);
        Invoke("RecalculateSize", 0.1f);        
    }
    void RecalculateSize()
    {
        container.SetActive(true);
        Vector2 s= GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = s;
    }

	public void OnOver () {
		Events.IsOverFunctionSlot (this);
        if(functionLine != null)
            image.color = Color.green;
        RecalculateSize();
    }
    public void OnExit()
    {
        RecalculateSize();
        if (functionLine != null)
            image.color = imageColor;
        Events.IsOverFunctionSlot (null);
    }

}
