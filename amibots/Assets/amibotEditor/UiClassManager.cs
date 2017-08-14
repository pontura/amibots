using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiClassManager : MonoBehaviour {

    public Transform container;
    public UIClassList uIClassList;
    public int id;
    public int separation = 200;
    void Start()
    {
        Events.OnUIClassSelected += OnUIClassSelected;
    }
    void OnUIClassSelected(AmiClass amiClass)
    {
        print("clicked: " + amiClass);
        if (amiClass.arguments.Count > 0)
        {
            print("clicked: " + amiClass.arguments.Count);
        }
        List<AmiClass> initClasses = new List<AmiClass>();
        foreach (AmiClass _amiClass in Data.Instance.amiClasses.classes)
        {
            if (_amiClass.type == AmiClass.types.SIMPLE_ACTION)
                initClasses.Add(_amiClass);
        }
        AddMenuList(initClasses);
    }
    public void Init()
    {
        List<AmiClass> initClasses = new List<AmiClass>();
        foreach (AmiClass amiClass in Data.Instance.amiClasses.classes)
        {
            if (amiClass.type == AmiClass.types.SIMPLE_ACTION)
                initClasses.Add(amiClass);
        }
        AddMenuList(initClasses);
    }
    void AddMenuList(List<AmiClass> newList)
    {
       
        UIClassList newClassList = Instantiate(uIClassList);
        newClassList.transform.SetParent(container);

        newClassList.OnAddListClasses(newList);
        newClassList.GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(id*-separation, 0);
        id++;
    }
}
