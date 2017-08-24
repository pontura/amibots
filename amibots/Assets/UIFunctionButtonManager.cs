using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionButtonManager : MonoBehaviour {

    public UIFunctionButton button;
    public Transform container;
    public string[] buttonClasses;

	void Start () {
        OnAddFunctions();
    }
    public void OnAddFunctions()
    {
        Utils.RemoveAllChildsIn(container);

        foreach (string  className in buttonClasses)
        {
            UIFunctionButton newButton = Instantiate(button);
            newButton.Init(className);
            newButton.transform.SetParent(container);
            newButton.transform.localScale = Vector3.one;
            newButton.transform.localPosition = Vector3.zero;
        }
    }
}
