using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

    public GameObject state1;
    public GameObject popup;
    public UiClassManager uiClassManager;
	public Button PlayButton;

	void Start () {
        state1.SetActive(true);
		popup.SetActive(false);
		PlayButton.interactable = false;
		Events.OnPopup += OnPopup;
		Events.OnPopupClose += OnPopupClose;
		Events.OnUIClassSelected += OnUIClassSelected;
	}
	void OnUIClassSelected(AmiClass a)
	{
		PlayButton.interactable = true;
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
	public void OnDebug()
	{
		Events.OnDebug (true);
	}
}
