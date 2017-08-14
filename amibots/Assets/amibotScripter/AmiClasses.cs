using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiClasses : MonoBehaviour {

    public List<AmiClass> classes;

	void Start () {
        AmiClass newClass;
        newClass = CreateNewClass("Walk", AmiClass.types.SIMPLE_ACTION, 0);
        newClass.arguments.Add(AmiClass.types.DIRECTION);
        newClass.arguments.Add(AmiClass.types.BODY_PART);
        newClass.arguments.Add(AmiClass.types.DISTANCE);
        newClass.arguments.Add(AmiClass.types.TIME);

        newClass = CreateNewClass("Talk", AmiClass.types.SIMPLE_ACTION, 1);
        newClass = CreateNewClass("Bye", AmiClass.types.SIMPLE_ACTION, 2);

        CreateNewClass( "forward", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "backward", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "up", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "down", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "left", AmiClass.types.DIRECTION, 0);
        CreateNewClass( "right", AmiClass.types.DIRECTION, 0);

        CreateNewClass( "right hand", AmiClass.types.BODY_PART, 0);
        CreateNewClass( "left hand", AmiClass.types.BODY_PART, 0);
        CreateNewClass( "right food", AmiClass.types.BODY_PART, 0);
        CreateNewClass( "left food", AmiClass.types.BODY_PART, 0);
        CreateNewClass( "body", AmiClass.types.BODY_PART, 2);
        CreateNewClass( "head", AmiClass.types.BODY_PART, 1);

        CreateNewClass("1", AmiClass.types.DISTANCE, 0);
        CreateNewClass("2", AmiClass.types.DISTANCE, 1);

        CreateNewClass( "1", AmiClass.types.TIME, 0);
        CreateNewClass( "2", AmiClass.types.TIME, 1);
        
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
