using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	
	public bool isWalkable;
//	Material material;
	MeshRenderer meshRenderer;
    public Vector2 pos;

	public void Init(bool isWalkable, Vector3 _pos)
	{
        pos = new Vector2(_pos.x, _pos.z);
        meshRenderer = GetComponent<MeshRenderer> ();
		this.isWalkable = isWalkable;
        _pos.x = (float)_pos.x + ((float)_pos.z * 0.32f);
		transform.localPosition = _pos;
		if (!isWalkable)
			SetAsUnwalkable ();
		else
			ResetPath ();
	}
    public Vector2 GetPos()
    {
        return new Vector2(transform.position.x, transform.position.z);
    }
	public void SetAsUnwalkable()
	{
		isWalkable = false;
		//material.color = Color.red;
		meshRenderer.enabled = true;
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
