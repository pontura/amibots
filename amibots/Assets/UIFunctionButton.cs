using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFunctionButton : MonoBehaviour {

    public string className;

	void Start () {
		
	}
    public void PointerDown()
    {
        print("done " + className);
    }
    public void PointerUp()
    {
        print("up " + className);
    }
    public void PointerExit()
    {
        print("exit " + className);
    }
}
