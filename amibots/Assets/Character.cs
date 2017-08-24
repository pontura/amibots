using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool isEditorCharacter;
	public GameObject body;
	public GameObject head;

	public GameObject foot_right;
	public GameObject foot_left;

	public GameObject hand_right;
	public GameObject hand_left;

	Animation anim;
	CharacterRulesToFall characterRulesToFall;
    public CharacterScriptsProcessor scriptsProcessor;

    public bool falled;

    public states state;
	public enum states
	{
		IDLE,
		FALL,
	}

	void Start () {
        scriptsProcessor = GetComponent<CharacterScriptsProcessor>();
        characterRulesToFall = GetComponent<CharacterRulesToFall> ();
		anim = GetComponent<Animation> ();
        if(isEditorCharacter)
		    Events.CharacterFall += CharacterFall;
	}

    void CharacterFall(string _anim)
	{
        falled = true;
        state = states.FALL;
		anim.Play (_anim);
	}
	public void UpdateFunctions(List<AmiClass> amiClasses, float timer)
	{		
		GameObject bodyPart = null;
		float distance = 1;
		float time = 1;
		foreach (AmiClass amiClass in amiClasses) {

			if (amiClass.type == AmiClass.types.DISTANCE) 
				distance = int.Parse(amiClass.className);
			if (amiClass.type == AmiClass.types.TIME) 
				time = int.Parse(amiClass.className);
			if (amiClass.type == AmiClass.types.BODY_PART) {
				switch (amiClass.className) {
				case "right foot":
					bodyPart = foot_right;
					break;
				case "left foot":
					bodyPart = foot_left;
					break;
				}
			}
			characterRulesToFall.Check (bodyPart, timer);
		}

		if (bodyPart != null) {
			Move (bodyPart, distance/time);
		}
	}

	Vector3 startingPos;
	public void Move(GameObject bodyPart, float qty)
	{
		if (qty == 0) 
			startingPos = bodyPart.transform.localPosition;
		
		//  Vector3 pos = bodyPart.transform.localPosition;
	    //	pos.z = qty + startingPos.z;
        //	bodyPart.transform.localPosition = pos;
        bodyPart.transform.Translate(Vector3.forward * (Time.deltaTime * qty));

        AlignBodyToFoots ();
	}
	public void Reset()
	{
        if (isEditorCharacter)
        {
          //  startingPos = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            Vector3 pos = transform.position;
            Vector3 destpos = body.transform.TransformPoint(Vector3.zero);
            pos.x = destpos.x;
            pos.z = destpos.z;
            startingPos = pos;
            transform.localPosition = pos;
           
        }
        print("IDLE");
		anim.Play ("idle");
        falled = false;
    }
	void AlignBodyToFoots()
	{
		Vector3 centerPos = body.transform.localPosition;
		centerPos.z = (foot_left.transform.localPosition.z + foot_right.transform.localPosition.z) / 2;
		body.transform.localPosition = centerPos;
	}
}
