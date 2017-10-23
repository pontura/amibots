using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

	public GameObject panel;
	public GameObject addCharacter;

	void Start () {
		panel.SetActive (true);
		addCharacter.SetActive (true);
		Events.AddCharacter += AddCharacter;
	}

	void AddCharacter (int id) {
		addCharacter.SetActive (false);
	}
}
