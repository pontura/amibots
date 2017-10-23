using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCustomizer : MonoBehaviour {

	public Customizer customizer;
	public GameObject panel;
	public GameObject button;

	void Start () {
		button.SetActive (false);
		panel.SetActive (false);
		Events.AddCharacter += AddCharacter;
	}

	void AddCharacter (int id) {
		button.SetActive (true);
	}
	public void Selected()
	{
		panel.SetActive (true);
		customizer.Init ();
	}
	public void Close()
	{
		panel.SetActive (false);
		customizer.SetOff ();
	}
}
