using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRulesToFall : MonoBehaviour {

	Character character;

	void Start()
	{
		character = GetComponent<Character> ();
	}
	public void Check(GameObject movedPart, float qty) {			
		CheckFor2FootsMoveing (movedPart, qty);
		CheckFor2FootsSeparation ();
	}


	float lastLeftFootMoved;
	float lastRightFootMoved;
	float timeBothFootsMoveing;

	void CheckFor2FootsMoveing(GameObject movedPart, float qty)
	{
		if (movedPart == character.foot_left) 
			lastLeftFootMoved = Time.time;
		if (movedPart == character.foot_right) 
			lastRightFootMoved = Time.time;	

		if (lastLeftFootMoved == lastRightFootMoved)
			timeBothFootsMoveing+=Time.deltaTime/2;

		if (timeBothFootsMoveing > 0.5f) {
			timeBothFootsMoveing = 0;
			Events.CharacterFall (Character.states.FALL_DOWN);
		}
	}


	void CheckFor2FootsSeparation()
	{
		if (Mathf.Abs ( character.foot_left.transform.localPosition.z - character.foot_right.transform.localPosition.z ) >1.4f)
			Events.CharacterFall (Character.states.FALL_FOOTS_SEPARATION);
	}
}
