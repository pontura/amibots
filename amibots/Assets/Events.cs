using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

    public static System.Action<string, Vector3> OnTooltip = delegate { };
    public static System.Action OnTooltipHide = delegate { };
    
    public static System.Action<List<UIFunctionLine>> SaveFunction = delegate { };
    public static System.Action<Vector3> ClickedOn = delegate { };
    public static System.Action ClickedOnScreen = delegate { };
    public static System.Action OnGameOver = delegate { };
    public static System.Action<AmiClass> OnUIClassSelected = delegate { };
	public static System.Action<AmiClass.types> OnPopup = delegate { };
	public static System.Action OnPopupClose = delegate { };
	public static System.Action<bool> OnDebug = delegate { };

	public static System.Action<string> DragStart = delegate { };
	public static System.Action<GameObject> DragStartGameObject = delegate { };
	public static System.Action DragEnd = delegate { };
	public static System.Action<UIFunctionSlot> IsOverFunctionSlot = delegate { };

	public static System.Action<Color> OnUIFunctionChangeIconColor = delegate { };

	public static System.Action<Character.states> CharacterFall = delegate { };
}
