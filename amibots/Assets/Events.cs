using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Events {

	public static System.Action OnGameOver = delegate { };
    public static System.Action<AmiClass> OnUIClassSelected = delegate { };
}
