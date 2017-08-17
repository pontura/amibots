using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiClassManager : MonoBehaviour {

    public Transform container;
    public UIClassList uIClassList;
    public int id;
    public int separation = 200;

	public UIFunctionLine functionLine;
	public Transform functionLineContainer;

    void Start()
    {
		Events.OnPopup += OnPopup;
		Events.OnUIClassSelected += OnUIClassSelected;
    }
	public void OnAddFunctionClicked()
	{
		Events.OnPopup (AmiClass.types.SIMPLE_ACTION);
	}
	void OnUIClassSelected(AmiClass amiClass)
    {
        if (amiClass.arguments.Count > 0)
        {
			AddFunction (amiClass);
        }

    }
	public void AddFunction(AmiClass amiClass)
	{
		UIFunctionLine newFunctionLine = Instantiate (functionLine);
		newFunctionLine.transform.SetParent (functionLineContainer);
		newFunctionLine.transform.localScale = Vector3.one;
		Vector3 pos = Input.mousePosition;
		pos.x = 0;
		newFunctionLine.transform.position = pos;
		newFunctionLine.Init(amiClass);
		newFunctionLine.gameObject.SetActive (true);
	}
	void OnPopup(AmiClass.types type)
	{
        List<AmiClass> initClasses = new List<AmiClass>();
        foreach (AmiClass amiClass in Data.Instance.amiClasses.classes)
        {
            if (amiClass.type == type)
                initClasses.Add(amiClass);
        }
        AddMenuList(initClasses);
    }
    void AddMenuList(List<AmiClass> newList)
    {
		uIClassList.OnAddListClasses(newList);
    }
}
