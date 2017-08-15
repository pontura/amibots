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

	void Start () {
		
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
		}

		if (bodyPart != null) {
			Move (bodyPart, timer*distance/time);
		}
	}
	public void Move(GameObject bodyPart, float qty)
	{
		Vector3 pos = bodyPart.transform.localPosition;
		pos.z = qty;
		bodyPart.transform.localPosition = pos;
	}
}
