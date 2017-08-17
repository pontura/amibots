using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIcons : MonoBehaviour {

	public GameObject move;
	public GameObject wait;

	public void Init(string className)
	{
		Reset ();
		switch (className) {
		case "Move":
			move.SetActive (true);
			break;
		case "Wait":
			wait.SetActive (true);
			break;
		}
	}
	void Reset()
	{
		move.SetActive (false);
		wait.SetActive (false);
	}
}
