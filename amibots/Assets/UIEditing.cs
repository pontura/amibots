using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEditing : MonoBehaviour {

    public GameObject state1;
    public GameObject popup;
    public UiClassManager uiClassManager;

	void Start () {
        state1.SetActive(true);
		popup.SetActive(false);
		Events.OnPopup += OnPopup;
		Events.OnPopupClose += OnPopupClose;
	}
	void OnPopup(AmiClass.types a)
	{
		popup.SetActive(true);
	}
	void OnPopupClose()
	{
		popup.SetActive(false);
	}
	public void ClosePopup()
	{		
		Events.OnPopupClose ();
	}
}
