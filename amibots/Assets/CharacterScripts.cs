using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterScripts : MonoBehaviour
{

    public List<AmiScript> scripts;

    void Start()
    {
        Events.SaveNewScript += SaveNewScript;
        Events.UpdateScript += UpdateScript;
    }
    void UpdateScript(AmiScript originalScript, AmiScript.categories category, string scriptName, List<UIFunctionLine> uifl)
    {
        AmiScript amiScript = CreateScriptFromLines(originalScript, uifl);
        amiScript.Init(scriptName);
        amiScript.category = category;
        Events.SetScriptSelected(amiScript);
       
    }
    void SaveNewScript(AmiScript.categories category, string scriptName, List<UIFunctionLine> uifl)
    {
        AmiScript amiScript = CreateScriptFromLines(new AmiScript(), uifl);
        amiScript.Init(scriptName);
        amiScript.category = category;
        Events.SetScriptSelected(amiScript);
        scripts.Add(amiScript);        
    }
    public AmiScript CreateScriptFromLines(AmiScript amiScript, List<UIFunctionLine> uifl)
    {
        amiScript.classes = new List<AmiClass>();
        int sequenceID = -1;
        foreach (UIFunctionLine line in uifl)
        {
            if (!line.isParallel)
            {
                print("::::::::::::");
                sequenceID++;
            }
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
           

        }
        return amiScript;
    }
}
