using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customizer : MonoBehaviour {
	
	void Start () {
		gameObject.SetActive (false);
	}

	public void Init() {
		gameObject.SetActive (true);
	}
	public void SetOff()
	{
		gameObject.SetActive (false);
	}
}
