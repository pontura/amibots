using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

    public Character character;

	void Start () {
        Events.ClickedOn += ClickedOn;

    }
	
	void ClickedOn(Vector3 pos) {
        character.transform.LookAt(pos);
	}
}
