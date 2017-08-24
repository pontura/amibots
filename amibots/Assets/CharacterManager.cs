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
        character.transform.LookAt(pos);
		Vector3 rot = character.transform.localEulerAngles;
		rot.x = 0;
		rot.z = 0;
		character.transform.localEulerAngles = rot;

        if(characterScripts.scripts.Count >0)
        {
           character.scriptsProcessor.ProcessScript(characterScripts.scripts[0]);
        }
	}
}
