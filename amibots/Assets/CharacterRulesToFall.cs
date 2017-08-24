using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRulesToFall : MonoBehaviour {

	Character character;

	void Start()
	{
		character = GetComponent<Character> ();
	}
	public void Check(GameObject movedPart) {	
		if (character.state == Character.states.FALL)
			return;
		CheckFor2FootsMoveing (movedPart);
		CheckFor2FootsSeparation ();
	}


	float lastLeftFootMoved;
	float lastRightFootMoved;
	float timeBothFootsMoveing;

	void CheckFor2FootsMoveing(GameObject movedPart)
	{
		if (movedPart == character.foot_left) 
			lastLeftFootMoved = Time.time;
		if (movedPart == character.foot_right) 
			lastRightFootMoved = Time.time;	

		if (lastLeftFootMoved == lastRightFootMoved)
			timeBothFootsMoveing += Time.deltaTime/2;

		if (timeBothFootsMoveing > 0.2f) {
			timeBothFootsMoveing = 0;
			Events.CharacterFall ("fall_2_foots");
		}
	}


	void CheckFor2FootsSeparation()
	{
		if (Mathf.Abs (character.foot_left.transform.localPosition.z - character.foot_right.transform.localPosition.z) > 1.1f) {
			Events.CharacterFall ("fall_foots_separation");
		//	print ("fall_foots_separation");
		}
    }
}
