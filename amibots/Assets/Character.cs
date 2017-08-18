using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	public GameObject body;
	public GameObject head;

	public GameObject foot_right;
	public GameObject foot_left;

	public GameObject hand_right;
	public GameObject hand_left;

	Animation anim;
	CharacterRulesToFall characterRulesToFall;

	public states state;
	public enum states
	{
		IDLE,
		FALL_DOWN,
		FALL_FOOTS_SEPARATION
	}

	void Start () {
		characterRulesToFall = GetComponent<CharacterRulesToFall> ();
		anim = GetComponent<Animation> ();
		Events.CharacterFall += CharacterFall;
	}
	void CharacterFall(states _state)
	{
		state = _state;
		switch (state) { 
		case states.FALL_DOWN:
			anim.Play ("fall_2_foots");
			break;
		case states.FALL_FOOTS_SEPARATION:
			anim.Play ("fall_foots_separation");
			break;
		}
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
				case "right food":
					bodyPart = foot_right;
					break;
				case "left food":
					bodyPart = foot_left;
					break;
				}
			}
			characterRulesToFall.Check (bodyPart, timer);
		}

		if (bodyPart != null) {
			Move (bodyPart, timer*distance/time);
		}
	}

	Vector3 startingPos;
	public void Move(GameObject bodyPart, float qty)
	{
		if (qty == 0) 
			startingPos = bodyPart.transform.localPosition;
		
		Vector3 pos = bodyPart.transform.localPosition;
		pos.z = qty + startingPos.z;
		bodyPart.transform.localPosition = pos;

		AlignBodyToFoots ();
	}
	public void Reset()
	{
		startingPos = Vector3.zero;
		anim.Play ("idle");
	}
	void AlignBodyToFoots()
	{
		Vector3 centerPos = body.transform.localPosition;
		centerPos.z = (foot_left.transform.localPosition.z + foot_right.transform.localPosition.z) / 2;
		body.transform.localPosition = centerPos;
	}
}
