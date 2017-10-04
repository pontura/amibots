using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	public types type;
	public enum types
	{
		FURNITURES,
		VEHICLES,
		SPORT
	}

	void Start()
	{
		OnStart ();
	}
	public void Init(Vector2 pos)
	{
		transform.position = new Vector3 (pos.x, 0, pos.y);
		OnInit ();
	}
	public virtual void OnStart() { }
	public virtual void OnInit() { }

}
