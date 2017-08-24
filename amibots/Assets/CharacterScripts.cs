using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterScripts : MonoBehaviour {

    public List<AmiScript> scripts;

    void Start () {
        Events.SaveScript += SaveScript;
    }
	
	void SaveScript(List<UIFunctionLine> uifl, string scriptName) {
        scripts.Clear();
        AmiScript amiScript = new AmiScript();
        amiScript.Init(scriptName);
        
        foreach (UIFunctionLine line in uifl)
            amiScript.lines.Add(line);

        scripts.Add(amiScript);
    }
}
