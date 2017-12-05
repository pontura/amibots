using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineMarker : MonoBehaviour {

	public float pos_x;

	public void SetX(float pos_x)
	{
		this.pos_x = pos_x;
		transform.localPosition = new Vector3 (pos_x, 0, 0);
	}
}
