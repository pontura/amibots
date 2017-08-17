using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionSlot : MonoBehaviour {

	public void OnOver () {
		Events.IsOverFunctionSlot (true);
	}
    public void OnExit()
    {
		Events.IsOverFunctionSlot (false);
    }

}
