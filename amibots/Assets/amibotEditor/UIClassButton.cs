using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIClassButton : MonoBehaviour {

    public Text field;
    AmiClass amiClass;
    public void Init(Transform parentContainer, AmiClass _amiClass)
    {
        this.amiClass = _amiClass;
        field.text = amiClass.className;
        transform.SetParent(parentContainer);
    }
    public void OnSelected()
    {
        Events.OnUIClassSelected(amiClass);
    }
}
