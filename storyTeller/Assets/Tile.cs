using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool isWalkable;

	public void Init(bool isWalkable, Vector3 pos)
	{
		this.isWalkable = isWalkable;
		transform.localPosition = pos;
		if (!isWalkable)
			transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);
	}
}
