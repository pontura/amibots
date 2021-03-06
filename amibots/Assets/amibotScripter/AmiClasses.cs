﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiClasses : MonoBehaviour {

    public List<AmiClass> classes;

	void Start () {
        AmiClass newClass;

        newClass = CreateNewClass("Move", AmiClass.types.SIMPLE_ACTION, 0);
		newClass.AddNewArgument(AmiClass.types.BODY_PART);
		newClass.AddNewArgument(AmiClass.types.DIRECTION);        
		newClass.AddNewArgument(AmiClass.types.DISTANCE);
		newClass.AddNewArgument(AmiClass.types.TIME);

        newClass = CreateNewClass("LookAt", AmiClass.types.SIMPLE_ACTION, 1);
		newClass.AddNewArgument(AmiClass.types.LOOK_AT_TARGET);

        newClass = CreateNewClass("Wait", AmiClass.types.SIMPLE_ACTION, 1);
		newClass.AddNewArgument(AmiClass.types.WAIT);

        newClass = CreateNewClass("Parallel", AmiClass.types.SIMPLE_ACTION, 1);

        newClass = CreateNewClass("Heads", AmiClass.types.SIMPLE_ACTION, 0);
        newClass.AddNewArgument(AmiClass.types.EXPRESSIONS);

        CreateNewClass("tap", AmiClass.types.LOOK_AT_TARGET, 0);
        CreateNewClass("opposite", AmiClass.types.LOOK_AT_TARGET, 0);

        CreateNewClass( "forward", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "backward", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "up", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "down", AmiClass.types.DIRECTION, 0);
     //   CreateNewClass( "left", AmiClass.types.DIRECTION, 0);
      //  CreateNewClass( "right", AmiClass.types.DIRECTION, 0);
        
        CreateNewClass( "right foot", AmiClass.types.BODY_PART, 0);
        CreateNewClass( "left foot", AmiClass.types.BODY_PART, 0);
		CreateNewClass( "right hand", AmiClass.types.BODY_PART, 1);
		CreateNewClass( "left hand", AmiClass.types.BODY_PART, 1);
        CreateNewClass( "hips", AmiClass.types.BODY_PART, 2);
        CreateNewClass( "head", AmiClass.types.BODY_PART, 3);

		CreateNewClass("10", AmiClass.types.DISTANCE, 0);
		CreateNewClass("25", AmiClass.types.DISTANCE, 0);
        CreateNewClass("50", AmiClass.types.DISTANCE, 0);
        CreateNewClass("100", AmiClass.types.DISTANCE, 0);
        CreateNewClass("200", AmiClass.types.DISTANCE, 1);

		CreateNewClass( "25", AmiClass.types.TIME, 0);
        CreateNewClass( "50", AmiClass.types.TIME, 0);
        CreateNewClass( "100", AmiClass.types.TIME, 1);

		CreateNewClass( "25", AmiClass.types.WAIT, 0);
		CreateNewClass( "5", AmiClass.types.WAIT, 0);
		CreateNewClass( "75", AmiClass.types.WAIT, 0);
		CreateNewClass( "100", AmiClass.types.WAIT, 0);
        //CreateNewClass( "AllDone", AmiClass.types.WAIT, 0);

        CreateNewClass("idle", AmiClass.types.EXPRESSIONS, 0);
        CreateNewClass("smile", AmiClass.types.EXPRESSIONS, 0);

    }
	public AmiClass GetClassesByClassName (string className) {
		foreach (AmiClass amiClass in classes) {
			if (amiClass.className == className)
				return amiClass;
		}
		return null;
	}
	public List<AmiClass> GetClassesByArg (AmiClass.types type) {
		List<AmiClass> classesByArg = new List<AmiClass> ();
		foreach (AmiClass amiClass in classes) {
			if (amiClass.type == type)
				classesByArg.Add( amiClass);
		}
		return classesByArg;
	}
    AmiClass CreateNewClass (string className, AmiClass.types type, int level) {
        AmiClass newClass = new AmiClass();
        newClass.className = className;
        newClass.type = type;
        newClass.level = level;
		newClass.argumentValues = new List<AmiArgument>();
        classes.Add(newClass);
        return newClass;
    }
	public string GetSentenceFor(string value, AmiClass.types type)
	{
		switch (type) {
		case AmiClass.types.DISTANCE:
			return value + " feets";
		case AmiClass.types.TIME:
			return "for " + (float.Parse(value)/100) + " sec";
		case AmiClass.types.WAIT:
			if(value == "AllDone")
				return "Wait for all done!";
			return "for " + (float.Parse(value)/100) + " sec";
		}
		return value;
	}
}
