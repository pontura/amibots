using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    public Character character;
    CharacterScripts characterScripts;

	void Start () {
        Events.ClickedOn += ClickedOn;
        characterScripts = CharacterData.Instance.characterScripts;
    }
	
	void ClickedOn(Vector3 pos) {
        character.transform.LookAt(pos);
        if(characterScripts.scripts.Count >0)
        {
            character.scriptsProcessor.ProcessScript(characterScripts.scripts[0]);
        }
	}
}
