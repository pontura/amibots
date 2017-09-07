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
	public int sequenceID;
	public bool isDone;

    public enum types
    {
        SIMPLE_ACTION,
        BODY_PART,
        DIRECTION,
        LOOK_AT_TARGET,
        DISTANCE,
        TIME,
		WAIT,
        PARALLEL,
        EXPRESSIONS
    }
    public List<AmiArgument> argumentValues;

	public void AddNewArgument(types type)
	{
		AmiArgument newArg = new AmiArgument ();
		newArg.type = type;
		argumentValues.Add (newArg);
	}
}