using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChatLine : MonoBehaviour {

	public TextMesh textMesh;

	void Start () {
		
	}
	public void Say(string _text)
	{
		textMesh.text = _text;
		Invoke ("Reset", 3);
	}
	void Reset()
	{
		textMesh.text = "";
	}
}
