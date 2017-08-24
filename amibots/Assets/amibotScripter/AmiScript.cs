using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AmiScript {

    public string scriptName;
    public List<UIFunctionLine> lines;

    public void Init(string _scriptName)
    {
        lines = new List<UIFunctionLine>();
        scriptName = _scriptName;
    }

}
