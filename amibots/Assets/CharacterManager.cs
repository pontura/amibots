using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    
    public Character character;
    CharacterScripts characterScripts;
	public UIGame UIGame;

	void Start () {
        Events.ClickedOn += ClickedOn;
        characterScripts = CharacterData.Instance.characterScripts;
    }
	
	void ClickedOn(Vector3 pos) {
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

        if (characterScripts.scripts.Count >0)
        {
           character.scriptsProcessor.ProcessScript(characterScripts.scripts[0]);
        }
	}
}
