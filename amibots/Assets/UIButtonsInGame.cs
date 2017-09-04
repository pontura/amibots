using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsInGame : MonoBehaviour {

    public UIButtonInGame buttonInGame;
    public Transform container;

	void Start () {
        Events.OnUIChangeState += OnUIChangeState;
    }
    void OnUIChangeState(UIGame.states state)
    {
        if (state == UIGame.states.PLAYING)
            RefreshButtons();
    }

    void RefreshButtons()
    {
        Utils.RemoveAllChildsIn(container);
        foreach(AmiScript amiScript in CharacterData.Instance.characterScripts.scripts)
        {
            UIButtonInGame newButton = Instantiate(buttonInGame);
            newButton.transform.SetParent(container);
            newButton.transform.localScale = Vector3.one;
            newButton.Init(this, amiScript);
        }
		UIButtonInGame[] buttons = container.gameObject.GetComponentsInChildren<UIButtonInGame> ();
		if(buttons.Length>0)
		SetSelected (buttons [buttons.Length - 1]);
    }
	public void SetSelected(UIButtonInGame button)
	{
		foreach(UIButtonInGame thisButton in container.gameObject.GetComponentsInChildren<UIButtonInGame>())
		{
			if (thisButton != button)
				thisButton.SetSelected (false);
			else {
				thisButton.SetSelected (true);
				Events.SetScriptSelected (thisButton.script);
			}
		}
	}
	public AmiScript GetScriptSelected()
	{
		foreach(UIButtonInGame thisButton in container.gameObject.GetComponentsInChildren<UIButtonInGame>())
		{
			if (thisButton.IsSelected ())
				return thisButton.script;
		}
		return null;
	}
}
