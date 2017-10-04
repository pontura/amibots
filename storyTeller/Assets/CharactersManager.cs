using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class CharactersManager : MonoBehaviour
{
    public float characterScale;
    public float separationInitial;

    public Character character_to_initialize;
    public Transform container;
    public Character selectedCharacter;

	public UIDragItem uiDragItem;

    void Start()
    {
        Events.AddCharacter += AddCharacter;
        Events.OnSelectCharacterID += OnSelectCharacterID;
        Events.OnCharacterAction += OnCharacterAction;
		Events.ClickedOn += ClickedOn;
		Events.OnCharacterSay += OnCharacterSay;
		Events.OnChangeExpression += OnChangeExpression;
		//Events.OnCharacterReachTile += OnCharacterReachTile;
    }
    void AddCharacter(int id)
    {
		
        Character character = Instantiate(character_to_initialize);
        character.transform.SetParent(container);
		character.transform.localPosition = World.Instance.tiles.GetFreeTileInCenter ();
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
	void ClickedOn(Tile tile)
	{		
		if (selectedCharacter && tile != null && !uiDragItem.isDragging) {
			List<Point> points = World.Instance.tiles.GetPathfinder (selectedCharacter.transform.position, tile.transform.position);
			if (points.Count > 0) {
				selectedCharacter.MoveFromPath (points);
			}
		}
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
