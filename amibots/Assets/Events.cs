using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action OnGameOver = delegate { };
    public static System.Action<AmiClass> OnUIClassSelected = delegate { };
	public static System.Action<AmiClass.types> OnPopup = delegate { };
	public static System.Action OnPopupClose = delegate { };
	public static System.Action<bool> OnDebug = delegate { };
}
