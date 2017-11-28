using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour {

    public Character character;
    CharacterData characterDataToDuplicate;

    void Start () {
		gameObject.SetActive (false);
	}

	public void Init(CharacterData data) {
        character.data = data;
        gameObject.SetActive(true);
        Invoke("Delayed", 0.1f);
	}
    void Delayed()
    {
        character.customizer.OnDupliacteCustomization(character.data);
    }
    public void SetOff()
	{
		gameObject.SetActive (false);
	}
}
