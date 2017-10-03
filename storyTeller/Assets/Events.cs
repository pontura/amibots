using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {


	public static System.Action<Character> OnCharacterReachTile = delegate { };
    public static System.Action<CharacterCustomizer.parts, string> OnCustomize = delegate { };
    public static System.Action<UIButton> OnUIButtonClicked = delegate { };
    
    public static System.Action<string, Vector3> OnTooltip = delegate { };
    public static System.Action<Tile> ClickedOn = delegate { };

    public static System.Action<int> AddCharacter = delegate { };

    public static System.Action<int> OnSelectCharacterID = delegate { };
    public static System.Action<Character> OnSelectCharacter = delegate { };

    public static System.Action<Settings.actions> OnCharacterAction = delegate { };
	public static System.Action<string> OnChangeExpression = delegate { };
	public static System.Action<string> OnCharacterSay= delegate { };
}
