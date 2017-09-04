using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterScripts : MonoBehaviour {
	
    public List<AmiScript> scripts;

    void Start () {
        Events.SaveNewScript += SaveNewScript;
        Events.UpdateScript += UpdateScript;
    }
    void UpdateScript(AmiScript originalScript, AmiScript.categories category, string scriptName, List<UIFunctionLine> uifl)
    {
		AmiScript amiScript = originalScript;
		Events.SetScriptSelected (amiScript);
		amiScript.Init(scriptName);
		amiScript.category = category;
		amiScript.classes = new List<AmiClass>();
		int sequenceID = 0;
		foreach (UIFunctionLine line in uifl)
		{
			AmiClass newClass = new AmiClass();
			newClass.className = line.field.text;
			newClass.argumentValues = new List<AmiArgument>();
			newClass.sequenceID = sequenceID;
			foreach (UIFunctionVarButton b in line.functionVarButtons)
			{
				AmiArgument arg = new AmiArgument();
				arg.type = b.arg;
				arg.value = b.GetValue();
				newClass.argumentValues.Add(arg);
			}
			amiScript.classes.Add(newClass);
			sequenceID++;
		}


    }
    void SaveNewScript(AmiScript.categories category, string scriptName, List<UIFunctionLine> uifl) {
        AmiScript amiScript = new AmiScript();
		Events.SetScriptSelected (amiScript);
        amiScript.Init(scriptName);
        amiScript.category = category;
        amiScript.classes = new List<AmiClass>();
		int sequenceID = 0;
        foreach (UIFunctionLine line in uifl)
        {
            AmiClass newClass = new AmiClass();
			newClass.className = line.field.text;
            newClass.argumentValues = new List<AmiArgument>();
			newClass.sequenceID = sequenceID;
            foreach (UIFunctionVarButton b in line.functionVarButtons)
            {
                AmiArgument arg = new AmiArgument();
				arg.type = b.arg;
                arg.value = b.GetValue();
                newClass.argumentValues.Add(arg);
            }
            amiScript.classes.Add(newClass);
			sequenceID++;
        }

        scripts.Add(amiScript);
    }
}
