using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEditing : MonoBehaviour {

    public GameObject panel;
    public GameObject tutorial;
    public GameObject state1;
    public GameObject popup;
    public UiClassManager uiClassManager;
	public Button PlayButton;
    public Text DebugText;
    public AmiScript.categories category;
    public string scriptName;
    public Text title;
    public AmiScript edittingScript;

	void Start () {
        tutorial.SetActive(true);
        state1.SetActive(true);
		popup.SetActive(false);
		PlayButton.interactable = false;
        Events.CreateNewEmptyScript += CreateNewEmptyScript;
        Events.OnUIChangeState += OnUIChangeState;
        Events.OnEditScript += OnEditScript;

        Events.OnPopup += OnPopup;
		Events.OnPopupClose += OnPopupClose;
		Events.OnUIClassSelected += OnUIClassSelected;
		Events.OnUIFunctionChangeIconColor += OnUIFunctionChangeIconColor;
        panel.SetActive(false);

    }
    void OnUIChangeState(UIGame.states state)
    {
        if (state == UIGame.states.PLAYING)
            panel.SetActive(false);
    }
    void OnEditScript(AmiScript amiScript)
    {
        this.edittingScript = amiScript;
        title.text = amiScript.category.ToString() + "->" + amiScript.scriptName;
        this.panel.SetActive(true);
        this.category = amiScript.category;
        this.scriptName = amiScript.scriptName;

        GetComponent<UiClassManager>().AddFunctionsFromScript(amiScript);
    }
    void CreateNewEmptyScript(AmiScript.categories _category, string _scriptName)
    {
        edittingScript = null;
        title.text = _category.ToString() + "->" + _scriptName;
        this.panel.SetActive(true);
        this.category = _category;
        this.scriptName = _scriptName;
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
    public void SaveFunction()
    {
        GetComponent<UITimeLine>().SaveFunction();
        GetComponent<UIGame>().BakToGame();
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
        {
            tutorial.SetActive(false);
            PlayButton.interactable = true;
        }
        else
        {
            PlayButton.interactable = false;
            DebugText.text = "Walk is empty";
            tutorial.SetActive(true);
        }
	}
}
