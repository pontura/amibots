﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiClasses : MonoBehaviour {

    public List<AmiClass> classes;

	void Start () {
        AmiClass newClass;
        newClass = CreateNewClass("Move", AmiClass.types.SIMPLE_ACTION, 0);

		newClass.arguments.Add(AmiClass.types.BODY_PART);
        newClass.arguments.Add(AmiClass.types.DIRECTION);        
        newClass.arguments.Add(AmiClass.types.DISTANCE);
        newClass.arguments.Add(AmiClass.types.TIME);

        newClass = CreateNewClass("Rotate", AmiClass.types.SIMPLE_ACTION, 1);

        CreateNewClass( "forward", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "backward", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "up", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "down", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "left", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "right", AmiClass.types.DIRECTION, 0);
        
        CreateNewClass( "right food", AmiClass.types.BODY_PART, 0);
        CreateNewClass( "left food", AmiClass.types.BODY_PART, 0);
		CreateNewClass( "right hand", AmiClass.types.BODY_PART, 1);
		CreateNewClass( "left hand", AmiClass.types.BODY_PART, 1);
        CreateNewClass( "body", AmiClass.types.BODY_PART, 2);
        CreateNewClass( "head", AmiClass.types.BODY_PART, 3);

        CreateNewClass("1", AmiClass.types.DISTANCE, 0);
        CreateNewClass("2", AmiClass.types.DISTANCE, 1);

        CreateNewClass( "1", AmiClass.types.TIME, 0);
        CreateNewClass( "2", AmiClass.types.TIME, 1);
        
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
        newClass.arguments = new List<AmiClass.types>();
        classes.Add(newClass);
        return newClass;
    }
}
