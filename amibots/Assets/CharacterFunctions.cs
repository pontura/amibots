using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFunctions : MonoBehaviour {

    public List<UIFunctionLine> functions;

    void Start () {
        Events.SaveFunction += SaveFunction;
    }
	
	void SaveFunction(List<UIFunctionLine> uifl) {
        functions.Clear();
        foreach (UIFunctionLine line in uifl)
            functions.Add(line);

    }
}
