using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiCustomizer : MonoBehaviour {

    public Character character_to_instantiate;
    public Customizer customizer;
	public GameObject panel;
	public GameObject button;
    public CreatedCharactersManager createdCharactersManager;

    void Start () {
		button.SetActive (false);
		panel.SetActive (false);
		Events.AddCharacter += AddCharacter;
	}
	void AddCharacter (int id) {
		button.SetActive (true);
	}
    bool isNewCharacter;
    public void CreateNew()
    {
        isNewCharacter = true;
        CharacterData data = new CharacterData();
        data.id = Data.Instance.charactersCreated.all.Count;
        Open(data);
    }
    public void Selected()
    {        
        Open(World.Instance.charactersManager.selectedCharacter.data);
    }
    public void Open(CharacterData data)
	{
		panel.SetActive (true);
		customizer.Init (data);
	}
	public void Close()
	{
        if (isNewCharacter)
        {
            Data.Instance.charactersCreated.CreateNew(customizer.character.data);
            CharacterCreated newCharacterCreated = createdCharactersManager.AddNewCharacter(customizer.character.data);
            GetComponent<UICharacterSelector>().AddNewCharacterCreated(newCharacterCreated);
        }
        isNewCharacter = false;
        panel.SetActive (false);
		customizer.SetOff ();
	}
    public CharacterData GetActiveCharacterData()
    {
        if (customizer.character != null)
            return customizer.character.data;
        return World.Instance.charactersManager.selectedCharacter.data;
    }
}
