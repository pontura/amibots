using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class CharactersManager : MonoBehaviour
{
    public float characterScale;
    public float separationInitial;

    public Character character_to_initialize;
    public Character selectedCharacter;
    public ScenesManager scenesManager;
	public UIDragItem uiDragItem;

	bool isRecording;

    void Start()
    {
        print("ASD");
		scenesManager = GetComponent<ScenesManager> ();
        Events.AddCharacter += AddCharacter;
        Events.RefreshCharacters += RefreshCharacters;
        Events.OnSelectCharacterID += OnSelectCharacterID;
        Events.OnCharacterAction += OnCharacterAction;
		Events.ClickedOn += ClickedOn;
		Events.OnChangeExpression += OnChangeExpression;
		Events.OnRecording += OnRecording;
		Events.NewSceneActive += NewSceneActive;
        Events.ClickedOnCharacter += ClickedOnCharacter;

    }
	void NewSceneActive(int id)
	{		
		if(scenesManager.sceneActive.characters.Count>0)
			selectedCharacter = scenesManager.sceneActive.characters [0];
	}
    void RefreshCharacters()
    {
        Invoke("RefreshCharactersDelayed", 0.25f);
    }
    public CharacterData data;
    int lastcharacterAddedID;
    void RefreshCharactersDelayed()
    {
        
        foreach (CharacterData data in World.Instance.createdCharactersManager.GetActiveCharacters())
        {
            this.data = data;
            if (!GetCharacter(data.id))
            {
                Events.AddCharacter(data);
                lastcharacterAddedID = data.id;
                Invoke("SelectLastAddedCharacter", 0.5f);
            }
        }
    }
    void SelectLastAddedCharacter()
    {
        OnSelectCharacterID(lastcharacterAddedID);
    }
    void AddCharacter(CharacterData data)
    {
        Character character = Instantiate(character_to_initialize);
		character.transform.SetParent(scenesManager.sceneActive.sceneObjects);
		character.transform.localPosition = World.Instance.scenesManager.sceneActive.tiles.GetFreeTileInCenter ();
        character.data = data;
        character.Init(data.id);
        character.transform.localScale = new Vector3(characterScale, characterScale, characterScale);
		character.transform.localEulerAngles = new Vector3 (35, 0, 0);

		Events.AddKeyFrameNewCharacter (character);

		scenesManager.sceneActive.characters.Add (character);
        
    }
	void OnChangeExpression(string value)
	{
		ChangeExpression(selectedCharacter.data.id, value);
		Events.AddKeyFrameExpression (selectedCharacter, value);
	}
	public void RestartScene()
	{
        if (scenesManager.sceneActive.characters.Count == 0) return;
		////////a mejorar:!
		selectedCharacter = scenesManager.sceneActive.characters [0];
		foreach (Character character in scenesManager.sceneActive.characters) {
			character.transform.position = new Vector3 (0, 1000, 0);
		}
	}
	public void ChangeExpression(int id, string value)
	{
		GetCharacter(id).customizer.OnChangeExpression(value);
	}
	void ClickedOn(Tile tile)
	{
        if (World.Instance.worldStates.state == WorldStates.states.SCENE_EDITOR)
            return;

		if (selectedCharacter && tile != null && !uiDragItem.isDragging) {

            foreach (Character character in scenesManager.sceneActive.characters)
            {
                if (Mathf.Round(character.transform.position.x) == Mathf.Round(tile.transform.position.x)
                    &&
                   Mathf.Round(character.transform.position.z) == Mathf.Round(tile.transform.position.z))
                    {
                   // OnSelectCharacterID(character.id);
                   // return;
                }
            }

            if (isRecording && selectedCharacter != null)
				Events.AddKeyFrameMove (selectedCharacter, tile.transform.position);

			MoveCharacter (selectedCharacter.data.id,  tile.transform.position);
		}
	}
    void ClickedOnCharacter(Character character)
    {
        OnSelectCharacterID(character.data.id);
    }
    void OnSelectCharacterID(int id)
    {
        selectedCharacter = GetCharacter(id);
        if (selectedCharacter)
            Events.OnSelectCharacter(selectedCharacter);

        foreach (Character character in scenesManager.sceneActive.characters)
        {
            if(character == selectedCharacter)
                character.SetSelected(true);
            else
                character.SetSelected(false);
        }
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
		foreach (Character character in scenesManager.sceneActive.sceneObjects.GetComponentsInChildren<Character>())
        {
            if (character.data.id == id)
                return character;
        }
        return null;
    }
    public int GetTotalCharacters()
    {
		return scenesManager.sceneActive.sceneObjects.GetComponentsInChildren<Character>().Length;
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
		if(World.Instance.scenesManager.sceneActive.characters.Count>0)
		{
			Character character = GetCharacter (id);
            List<Point> coords = World.Instance.scenesManager.sceneActive.tiles.GetPathfinder (character.transform.position, moveTo);
            List<Vector3> pos = new List<Vector3>();
            foreach (Point p in coords)
                pos.Add(World.Instance.scenesManager.sceneActive.tiles.GetPositionsByPoints(p));
           // return;
            // List<Vector3> coords = World.Instance.scenesManager.sceneActive.tiles.PathfinderToPosition(points);
            if (coords.Count > 0) {
                GetCharacter(id).MoveFromPath (pos);
            }
        }
	}
}
