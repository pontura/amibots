using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AmiClass 
{
    public string className;
    public int level;
    public types type;

    [HideInInspector]
    public List<types> arguments;

    public enum types
    {
        SIMPLE_ACTION,
        BODY_PART,
        DIRECTION,
        LOOK_AT_TARGET,
        DISTANCE,
        TIME,
		WAIT,
        PARALLEL
    }
    public List<AmiArgument> argumentValues;
}