using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour {

    public Character character;

	void Start () {
		gameObject.SetActive (false);
	}

	public void Init() {
        gameObject.SetActive(true);
        Invoke("Delayed", 0.1f);
	}
    void Delayed()
    {
        character.customizer.OnDupliacteCustomization(World.Instance.charactersManager.selectedCharacter.customizer);

    }
    public void SetOff()
	{
		gameObject.SetActive (false);
	}
}
