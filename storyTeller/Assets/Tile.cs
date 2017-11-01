using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	
	public bool isWalkable;
//	Material material;
	MeshRenderer meshRenderer;

	public void Init(bool isWalkable, Vector3 pos)
	{
		meshRenderer = GetComponent<MeshRenderer> ();
		meshRenderer.enabled = false;
		//material.
		this.isWalkable = isWalkable;
		transform.localPosition = pos;
		if (!isWalkable)
			SetAsUnwalkable ();
		else
			ResetPath ();
	}
	public Vector2 GetVector2()
	{
		return new Vector2(transform.position.x, transform.position.z);
	}
	public void SetAsUnwalkable()
	{
		isWalkable = false;
		//material.color = Color.red;
		//meshRenderer.enabled = true;
	}
	public void SetAsWalkable()
	{
		isWalkable = true;
		ResetPath ();
		meshRenderer.enabled = false;
	}
	public void MarkAsPath()
	{
		//material.color = Color.yellow;
	}
	public void ResetPath()
	{
		//if (isWalkable)
		//	material.color = Color.green;
	}
}
