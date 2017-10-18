using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExpresionsManager : MonoBehaviour {

	public GameObject panel;

	void Start () {
		panel.SetActive (false);
		Events.AddCharacter += AddCharacter;
	}
	void OnDestroy () {
		Events.AddCharacter -= AddCharacter;
	}
	void AddCharacter(int id)
	{
		panel.SetActive (true);
	}
}
