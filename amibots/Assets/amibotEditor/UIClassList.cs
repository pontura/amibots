using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClassList : MonoBehaviour {

    public UIClassButton button;
    public Transform container;

    public void OnAddListClasses(List<AmiClass> amiClasses)
    {
		Utils.RemoveAllChildsIn (container);

        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;

        foreach (AmiClass amiClass in amiClasses)
        {
            UIClassButton newButton = Instantiate(button);
            newButton.Init(container, amiClass);
			newButton.transform.localScale = Vector3.one;
        }        
    }
}
