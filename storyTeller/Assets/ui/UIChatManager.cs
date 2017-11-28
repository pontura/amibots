using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChatManager : MonoBehaviour {

	public GameObject panel;
	public InputField input;
	public Transform container;
	public UIChat uiChat_to_instantiate;

	void Start () {
		Events.OnUIButtonClicked += OnUIButtonClicked;
		Events.AddCharacter += AddCharacter;
		Events.OnCharacterSay += OnCharacterSay;
		panel.SetActive (false);
	}
	void OnDestroy () {
		Events.OnUIButtonClicked -= OnUIButtonClicked;
		Events.AddCharacter -= AddCharacter;
	}
	void AddCharacter(int id)
	{
		panel.SetActive (true);
	}
	void OnUIButtonClicked(UIButton uiButton)
	{
		switch (uiButton.type)
		{
		case UIButton.types.CHAT:
			Events.OnCharacterSay (World.Instance.charactersManager.selectedCharacter.data.id, input.text);
			input.text = "";
			break;
		}
	}
	public void OnCharacterSay(int avatarID, string text)
	{
		UIChat chat = Instantiate (uiChat_to_instantiate);
		chat.transform.SetParent (container);
		chat.transform.localScale = Vector3.one;
        chat.transform.localEulerAngles = Vector3.zero;

        chat.Init (avatarID, text);
	}
}
