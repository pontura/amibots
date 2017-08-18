using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actionbutton : MonoBehaviour {

	Animation anim;
	public Image colorizable;

	void Start () {
		anim = GetComponent<Animation> ();
	}
	public void Activate() {
		anim.Play ("late");	
	}
	public void ChangeColor(Color _color)
	{
		colorizable.color = _color;
	}
}
