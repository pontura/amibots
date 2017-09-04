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
        if (amiClass.argumentValues.Count > 0)
        {
			AddFunction (amiClass, functionLineContainer);
        }
    }
	public void RepositionateFunction(GameObject go)
	{
		//go.transform.SetParent (functionLineContainer);
	}
    public void AddFunctionsFromScript(AmiScript amiScript)
    {
		foreach (AmiClass amiClass in amiScript.classes) {
			AddFunction (amiClass, functionLineContainer);
		}
    }
	public void AddFunction(AmiClass amiClass, Transform _container)
	{        
		UIFunctionLine newFunctionLine = Instantiate (functionLine);
        newFunctionLine.gameObject.SetActive(true);
        newFunctionLine.transform.SetParent (_container);
		newFunctionLine.transform.localScale = Vector3.one;

		//print ("argumentValues " + amiClass.argumentValues.Count + " _container " + _container);

		newFunctionLine.Init(amiClass);
      //  if (_container.name == "FunctionSlot_Childs")
      //  {
       //     newFunctionLine.SetParallelOf( _container.GetComponent<UIFunctionSlot>().functionLine );
      //  }
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
