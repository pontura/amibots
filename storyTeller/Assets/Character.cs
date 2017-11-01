﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;
using Anima2D;

public class Character : MonoBehaviour {

	public float speed;
    public int id;
    public bool isEditorCharacter;

    public GameObject allBody;

	Animation anim;
    public bool falled;
	List<int> orders;
	List<GameObject> allParts;

    public Vector3 lookAtTarget;

    public CharacterActionsManager actions;
	public CharacterChatLine chatLine;
	public CharacterCustomizer customizer;

	public states state;
    public enum states
	{
		IDLE,
		MOVEING,
	}

	void Awake () {
        anim = GetComponent<Animation> ();
        actions = GetComponent<CharacterActionsManager>();
		chatLine = GetComponent<CharacterChatLine> ();
		customizer = GetComponent<CharacterCustomizer> ();
    }
	void Start()
	{
		allParts = new List<GameObject> ();
		orders = new List<int> ();

		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
			allParts.Add(sr.gameObject);	
			orders.Add (sr.sortingOrder);
		}
		foreach (SpriteMeshInstance sr in GetComponentsInChildren<SpriteMeshInstance>()) {
			allParts.Add(sr.gameObject);
			orders.Add (sr.sortingOrder);
		}

	}
    void Update()
    {
		if (newPos == Vector3.zero)
			return;
		if (state != states.MOVEING) {
			actions.Walk ();
			state = states.MOVEING;
		}
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPos, speed*Time.deltaTime);

		float dist = Vector3.Distance (transform.localPosition, newPos);
		if (dist < 0.01f) {
			CharacterReachTile ();
			//Events.OnCharacterReachTile (this);
			//Reset ();
		}
    }
    public void Init(int id)
    {
        this.id = id;
		lookAtTarget = World.Instance.scenesManager.sceneActive.cam.transform.localPosition;
    }
	int pathStep;
	List<Point> points;
	public void MoveFromPath(List<Point> points)
	{
		this.points = points;
		pathStep = 0;
		CharacterReachTile ();
	}
	void CharacterReachTile()
	{
		ReorderInLayers ();
	//	print ("CharacterReachTile  points.Count: " + points.Count + "   pathStep: " + pathStep);
		if (pathStep >= points.Count) {
			Events.OnCharacterReachTile (this);
			Reset ();
			return;
		}
		pathStep++;
		Vector3 newPos = new Vector3 (points [pathStep-1].x, 0, points [pathStep-1].y);
		Move (newPos);

	}
	Vector3 newPos;
	public void Move(Vector3 _newPos)
	{
		Vector3 newScale = allBody.transform.localScale;
		if ((_newPos.x > transform.position.x && allBody.transform.localScale.x>0) || (_newPos.x < transform.position.x && allBody.transform.localScale.x<0)) {
			newScale.x *= -1;
		}
		allBody.transform.localScale = newScale;
		newPos = _newPos;
	}
	public void Reset()
	{      
		newPos = Vector3.zero;
		actions.Idle ();
        falled = false;
		state = states.IDLE;
    }
	void ReorderInLayers()
	{
		int order = (int)transform.position.z;
		int a = 0;
		foreach (GameObject s in allParts) {
			SpriteRenderer sr = s.GetComponent<SpriteRenderer> ();
			if (sr) {
				sr.sortingOrder = (-100 * order) + orders [a];	
			} else {
				SpriteMeshInstance smr = s.GetComponent<SpriteMeshInstance> ();
				if (smr)
					smr.sortingOrder = (-100 * order) + orders [a];	
			}
			a++;
		}
	}


}
