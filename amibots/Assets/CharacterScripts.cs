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
        foreach (AmiScript amiScript in scripts)
        {
            if (amiScript == originalScript)
            {
            }
        }
    }
    void SaveNewScript(AmiScript.categories category, string scriptName, List<UIFunctionLine> uifl) {
        AmiScript amiScript = new AmiScript();
        amiScript.Init(scriptName);
        amiScript.category = category;
        amiScript.classes = new List<AmiClass>();
        
        foreach (UIFunctionLine line in uifl)
        {
            print(line.parallelOf);
            AmiClass newClass = new AmiClass();
            newClass.argumentValues = new List<AmiArgument>();
            foreach (UIFunctionVarButton b in line.functionVarButtons)
            {
                AmiArgument arg = new AmiArgument();
                arg.argument = b.arg;
                arg.value = b.GetValue();
                newClass.argumentValues.Add(arg);
            }
            amiScript.classes.Add(newClass);
        }

        scripts.Add(amiScript);
    }
}
