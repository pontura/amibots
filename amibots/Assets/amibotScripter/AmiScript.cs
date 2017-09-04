using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AmiScript {

    public categories category;
    public enum categories
    {
        ON_TAP,
        AVATAR_ACTION
    }
    public string scriptName;
    public List<AmiClass> classes;


    public void Init(string _scriptName)
    {
       // lines = new List<UIFunctionLine>();
        scriptName = _scriptName;
    }

}
