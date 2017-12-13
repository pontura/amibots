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
	void AddCharacter (CharacterData data) {
		button.SetActive (true);
	}
    bool isNewCharacter;
    public void CreateNew(bool _isNewCharacter)
    {
        isNewCharacter = _isNewCharacter;
        CharacterData data = new CharacterData();
        data.id = Data.Instance.charactersCreated.all.Count;

        int character_id = data.id;

        data.hairs = "hair_" + Random.Range(1,4);
        data.clothes = "ropa_top_" + Random.Range(1, 3);
        data.legs = "ropa_bottom_" + Random.Range(1, 3);       
        data.colors = "" + Random.Range(1, 5);

        if(isNewCharacter)
            Open(data);
        else
            customizer.Init(data);
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
