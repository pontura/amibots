using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    public float characterScale;
    public float separationInitial;

    public Character character_to_initialize;
    public Transform container;
    public Character selectedCharacter;

    void Start()
    {
        Events.AddCharacter += AddCharacter;
        Events.OnSelectCharacterID += OnSelectCharacterID;
        Events.OnCharacterAction += OnCharacterAction;
		Events.ClickedOn += ClickedOn;
		Events.OnCharacterSay += OnCharacterSay;
		Events.OnChangeExpression += OnChangeExpression;
    }
    void AddCharacter(int id)
    {
        Character character = Instantiate(character_to_initialize);
        character.transform.SetParent(container);
        character.transform.localPosition = new Vector3(GetTotalCharacters()*separationInitial, 0, 0);
        character.Init(id);
        character.transform.localScale = new Vector3(characterScale, characterScale, characterScale);
    }
	void OnChangeExpression(string value)
	{
		if (selectedCharacter)
			selectedCharacter.customizer.OnChangeExpression(value);
	}
	void OnCharacterSay(string text)
	{
		if (selectedCharacter)
			selectedCharacter.chatLine.Say(text);
	}
	void ClickedOn(Vector3 pos)
	{
		if (selectedCharacter)
			selectedCharacter.Move(pos);
	}
    void OnSelectCharacterID(int id)
    {
        selectedCharacter = GetCharacter(id);
        if (selectedCharacter)
            Events.OnSelectCharacter(selectedCharacter);
    }
    void OnCharacterAction(Settings.actions action)
    {
        if (selectedCharacter == null) return;
        selectedCharacter.actions.Set(action);
    }
    Character GetCharacter(int id)
    {
        foreach (Character character in container.GetComponentsInChildren<Character>())
        {
            if (character.id == id)
                return character;
        }
        return null;
    }
    public int GetTotalCharacters()
    {
        return container.GetComponentsInChildren<Character>().Length;
    }
}
