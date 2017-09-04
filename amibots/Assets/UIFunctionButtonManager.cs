using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionButtonManager : MonoBehaviour {

    public UIFunctionButton button;
    public Transform container;

	void Start () {
        Invoke("OnAddFunctions", 0.5f);
    }
    public void OnAddFunctions()
    {
        Utils.RemoveAllChildsIn(container);

        foreach (AmiClass  amiClass in Data.Instance.amiClasses.classes)
        {
            if (amiClass.type == AmiClass.types.SIMPLE_ACTION)
            {
                UIFunctionButton newButton = Instantiate(button);

                newButton.Init(amiClass.className);
                newButton.transform.SetParent(container);
                newButton.transform.localScale = Vector3.one;
                newButton.transform.localPosition = Vector3.zero;
            }
        }
    }
}
