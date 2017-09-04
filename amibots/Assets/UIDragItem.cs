using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragItem : MonoBehaviour {

	public UIIcons _icons;
	private UIIcons icons;
    public Transform dragSimpleIconsContainer;

	bool isOn;

	void Start()
	{
		
		Events.DragStart += DragStart;
		Events.DragEnd += DragEnd;
       

        Events.DragStartGameObject += DragStartGameObject;
		icons = Instantiate (_icons);
		icons.transform.SetParent (transform);
		icons.transform.localPosition = Vector3.zero;

        DragEnd();
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
        Utils.RemoveAllChildsIn(dragSimpleIconsContainer);
        icons.gameObject.SetActive(true);
    }
	void DragStartGameObject(GameObject go)
	{
        icons.gameObject.SetActive(false);
        //icons.transform.localPosition = new Vector3 (1000, 0, 0);
        isOn = true;
		go.transform.SetParent (dragSimpleIconsContainer.transform);
		go.transform.localPosition =new Vector3(253, -28,0);
	}
}
