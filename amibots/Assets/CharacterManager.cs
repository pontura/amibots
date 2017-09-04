using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    
    public Character character;
    CharacterScripts characterScripts;
	public UIGame UIGame;
	public UIButtonsInGame uiButtonsInGame;
	AmiScript script;

	void Start () {
        Events.ClickedOn += ClickedOn;
		Events.SetScriptSelected += SetScriptSelected;
        characterScripts = CharacterData.Instance.characterScripts;
    }
	void SetInvoked()
	{
		script = uiButtonsInGame.GetScriptSelected ();
	}
	void SetScriptSelected(AmiScript _script)
	{
		this.script = _script;
	}
	void ClickedOn(Vector3 pos) {
		
		if(script == null)
			return;
		
		if (UIGame.state == UIGame.states.EDITING)
			return;
		character.Reset ();
        character.pivot.transform.LookAt(pos);
       Vector3 rot = character.pivot.transform.localEulerAngles;
        rot.x = rot.z = 0;
        character.pivot.transform.localEulerAngles = rot;

        if (pos.x > character.transform.localPosition.x)
            character.transform.localScale = new Vector3(-1, 1, 1);
        else
            character.transform.localScale = new Vector3(1, 1, 1);
		
		character.scriptsProcessor.ProcessScript(script);
	}
}
