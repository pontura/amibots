using System.Collections;
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



        newClass = CreateNewClass("Wait", AmiClass.types.SIMPLE_ACTION, 1);
		newClass.arguments.Add(AmiClass.types.WAIT);



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

		CreateNewClass( "1", AmiClass.types.WAIT, 0);
		CreateNewClass( "2", AmiClass.types.WAIT, 0);
		//CreateNewClass( "AllDone", AmiClass.types.WAIT, 0);
        
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
        newClass.arguments = new List<AmiClass.types>();
        classes.Add(newClass);
        return newClass;
    }
	public string GetSentenceFor(string value, AmiClass.types type)
	{
		switch (type) {
		case AmiClass.types.DISTANCE:
			return value + " feets";
			break;
		case AmiClass.types.TIME:
			return "for " + value + " sec";
			break;
		case AmiClass.types.WAIT:
			if(value == "AllDone")
				return "Wait for all done!";
			return "for " + value + " sec";
			break;
		}
		return value;
	}
}
