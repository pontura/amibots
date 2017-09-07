using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

    public static System.Action<CharacterCustomizer.parts, string> OnCustomize = delegate { };
    public static System.Action<AmiScript.categories> EditNameOfAction = delegate { };
    public static System.Action<AmiScript.categories, string> CreateNewEmptyScript = delegate { };
    public static System.Action<AmiScript> OnEditScript = delegate { };

    public static System.Action<string, Vector3> OnTooltip = delegate { };
    public static System.Action OnTooltipHide = delegate { };
    
    public static System.Action<AmiScript.categories, string, List<UIFunctionLine>> SaveNewScript = delegate { };
    public static System.Action<AmiScript, AmiScript.categories, string, List<UIFunctionLine>> UpdateScript = delegate { };
    public static System.Action<Vector3> ClickedOn = delegate { };
	public static System.Action<AmiScript> SetScriptSelected = delegate { };
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
    public static System.Action<UIGame.states> OnUIChangeState = delegate { };

    public static System.Action<string> CharacterFall = delegate { };
    public static System.Action OpenCategorySelector = delegate { };
}
