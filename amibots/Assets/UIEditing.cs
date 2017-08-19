using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

    public GameObject state1;
    public GameObject popup;
    public UiClassManager uiClassManager;
	public Button PlayButton;
	public Image actionIcon;
    public Text DebugText;

	void Start () {
        state1.SetActive(true);
		popup.SetActive(false);
		PlayButton.interactable = false;
		Events.OnPopup += OnPopup;
		Events.OnPopupClose += OnPopupClose;
		Events.OnUIClassSelected += OnUIClassSelected;
		Events.OnUIFunctionChangeIconColor += OnUIFunctionChangeIconColor;

    }
	void OnUIFunctionChangeIconColor(Color color)
	{
        if (color == Color.grey)
            DebugText.text = "playing...";
        else if (color == Color.yellow)
            DebugText.text = "Walk incomplete";
        else if (color == Color.red)
            DebugText.text = "Walk has Errors!";
        else if (color == Color.green)
            DebugText.text = "Walk is Done!";

        actionIcon.color = color;
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
	void Update()
	{
        if (uiClassManager.functionLineContainer.GetComponentInChildren<UIFunctionLine>())
            PlayButton.interactable = true;
        else
        {
            PlayButton.interactable = false;
            DebugText.text = "Walk is empty";
        }
	}
}
