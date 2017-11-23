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
        Events.OnUIButtonClicked += OnUIButtonClicked;

    }
    void OnUIButtonClicked(UIButton u)
    {
        AddCharacter(0);
    }

    void AddCharacter (int id) {
        Events.AddCharacter -= AddCharacter;
        Events.OnUIButtonClicked -= OnUIButtonClicked;
        addCharacter.SetActive (false);
	}
}
