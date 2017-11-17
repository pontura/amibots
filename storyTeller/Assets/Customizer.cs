using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour {

    public Character character;

	void Start () {
		gameObject.SetActive (false);
	}

	public void Init() {
        character.customizer.OnDupliacteCustomization(World.Instance.charactersManager.selectedCharacter.customizer);
        gameObject.SetActive (true);
	}
	public void SetOff()
	{
		gameObject.SetActive (false);
	}
}
