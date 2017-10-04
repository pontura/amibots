using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public bool isWalkable;
	Material material;

	public void Init(bool isWalkable, Vector3 pos)
	{
		material = GetComponent<MeshRenderer> ().material;
		this.isWalkable = isWalkable;
		transform.localPosition = pos;
		if (!isWalkable)
			SetAsUnwalkable ();
		else
			ResetPath ();
	}
	public void SetAsUnwalkable()
	{
		material.color = Color.red;
	}
	public void MarkAsPath()
	{
		material.color = Color.yellow;
	}
	public void ResetPath()
	{
		if (isWalkable)
			material.color = Color.green;
	}
}
