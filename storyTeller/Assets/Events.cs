using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action<Tile, bool> Blocktile = delegate { };
	public static System.Action<SceneObjectData> OnDrag = delegate { };
	public static System.Action OnEndDrag = delegate { };

	public static System.Action<Character> OnCharacterReachTile = delegate { };
    public static System.Action<CharacterCustomizer.parts, string> OnCustomize = delegate { };
    public static System.Action<UIButton> OnUIButtonClicked = delegate { };

	public static System.Action<bool> OnRecording = delegate { }; 
	public static System.Action<bool> OnPlaying = delegate { }; 
    public static System.Action<string, Vector3> OnTooltip = delegate { };
    public static System.Action<Tile> ClickedOn = delegate { };

	public static System.Action<Character> AddKeyFrameNewCharacter = delegate { };
	public static System.Action<Character, string> AddKeyFrameAction = delegate { };
	public static System.Action<Character, string> AddKeyFrameExpression = delegate { };
	public static System.Action<Character, Vector3> AddKeyFrameMove = delegate { };
    public static System.Action<int> AddCharacter = delegate { };
	public static System.Action<SceneObjectData, Vector2> AddGenericObject = delegate { };


    public static System.Action<int> OnSelectCharacterID = delegate { };
    public static System.Action<Character> OnSelectCharacter = delegate { };

	public static System.Action<string> OnCharacterAction = delegate { };
	public static System.Action<string> OnChangeExpression = delegate { };
	public static System.Action<int, string> OnCharacterSay= delegate { };
	public static System.Action OnTimelineUpdated= delegate { };

}
