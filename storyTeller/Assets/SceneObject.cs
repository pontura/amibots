using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObject : MonoBehaviour {

	public types type;
	public enum types
	{
		BACKGROUND,
		FURNITURES,
		VEHICLES,
		SPORT
	}
	public SceneObjectData data;
	List<int> orders;
	void Start()
	{
		OnStart ();
	}
	public void Init(SceneObjectData data, Vector2 pos)
	{
		this.data = data;
		transform.position = new Vector3 (pos.x, 0, pos.y);
		OnInit ();
		orders = new List<int> ();
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
			orders.Add(sr.sortingOrder);		
		}
		ReorderInLayers ();
	}
	void ReorderInLayers()
	{
		int order = (int)transform.position.z;
		int a = 0;
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
			sr.sortingOrder = (-100*order) + orders[a];	
			a++;
		}
	}
	public virtual void OnStart() { }
	public virtual void OnInit() { }

}
