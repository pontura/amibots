using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class CharactersManager : MonoBehaviour
{
	public List<Character> characters;
    public float characterScale;
    public float separationInitial;

    public Character character_to_initialize;
    public Transform container;
    public Character selectedCharacter;

	public UIDragItem uiDragItem;
	bool isRecording;

    void Start()
    {
        Events.AddCharacter += AddCharacter;
        Events.OnSelectCharacterID += OnSelectCharacterID;
        Events.OnCharacterAction += OnCharacterAction;
		Events.ClickedOn += ClickedOn;
		Events.OnChangeExpression += OnChangeExpression;
		Events.OnRecording += OnRecording;
    }
    void AddCharacter(int id)
    {
        Character character = Instantiate(character_to_initialize);
        character.transform.SetParent(container);
		character.transform.localPosition = World.Instance.tiles.GetFreeTileInCenter ();
        character.Init(id);
        character.transform.localScale = new Vector3(characterScale, characterScale, characterScale);

		if(characters.Count>0)
			Events.AddKeyFrameNewCharacter (character);

		characters.Add (character);
    }
	void OnChangeExpression(string value)
	{
		ChangeExpression(selectedCharacter.id, value);
		Events.AddKeyFrameExpression (selectedCharacter, value);
	}
	public void RestartScene()
	{
		////////a mejorar:!
		selectedCharacter = characters [0];
		foreach (Character character in characters) {
			character.transform.position = new Vector3 (0, 1000, 0);
		}
	}
	public void ChangeExpression(int id, string value)
	{
		GetCharacter(id).customizer.OnChangeExpression(value);
	}
	void ClickedOn(Tile tile)
	{		
		if (selectedCharacter && tile != null && !uiDragItem.isDragging) {
			
			if (isRecording && selectedCharacter != null)
				Events.AddKeyFrameMove (selectedCharacter, tile.transform.position);

			MoveCharacter (selectedCharacter.id,  tile.transform.position);
		}
	}
    void OnSelectCharacterID(int id)
    {
        selectedCharacter = GetCharacter(id);
        if (selectedCharacter)
            Events.OnSelectCharacter(selectedCharacter);
    }
	void OnCharacterAction(string value)
    {
        if (selectedCharacter == null) return;
		selectedCharacter.actions.Set(value);
		Events.AddKeyFrameAction (selectedCharacter, value);
    }
	public void CharacterAction(int id, string value)
	{
		GetCharacter(id).actions.Set(value);
	}
	public Character GetCharacter(int id)
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
	void OnRecording(bool isRecording)
	{
		this.isRecording = isRecording;
	}
	public void PositionateCharacter(int id, Vector3 pos)
	{
		GetCharacter (id).transform.position = pos;
	}
	public void MoveCharacter(int id, Vector3 moveTo)
	{
		Character character = GetCharacter (id);
		selectedCharacter = character;
		List<Point> points = World.Instance.tiles.GetPathfinder (character.transform.position, moveTo);
		if (points.Count > 0) {
			GetCharacter(id).MoveFromPath (points);
		}
	}
}
