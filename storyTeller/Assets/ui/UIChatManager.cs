using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChatManager : MonoBehaviour {

	public InputField input;

	void Start () {
		Events.OnUIButtonClicked += OnUIButtonClicked;
	}
	void OnUIButtonClicked(UIButton uiButton)
	{
		switch (uiButton.type)
		{
		case UIButton.types.CHAT:
			Events.OnCharacterSay (input.text);
			input.text = "";
			break;
		}
	}
}
