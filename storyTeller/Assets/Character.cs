using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;
using Anima2D;

public class Character : MonoBehaviour {

    public Avatar avatar_to_instantiate;
    CharacterSelector characterSelector;
    [HideInInspector]
    public Avatar avatar;
	public float speed;
    public int id;
    public bool isEditorCharacter;
    
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
        characterSelector = GetComponent<CharacterSelector>();
    }
    public void SetSelected(bool isSelected)
    {
        characterSelector.SetState(isSelected);
    }
	void Start()
	{
        avatar = Instantiate(avatar_to_instantiate);
        avatar.transform.SetParent(transform);
        avatar.transform.localScale = Vector3.one;
        avatar.transform.localPosition = Vector3.zero;
        avatar.transform.localEulerAngles = Vector3.zero;
        allParts = new List<GameObject> ();
		orders = new List<int> ();
        actions.Init();

        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
			allParts.Add(sr.gameObject);	
			orders.Add (sr.sortingOrder);
		}
		foreach (SpriteMeshInstance sr in GetComponentsInChildren<SpriteMeshInstance>()) {
			allParts.Add(sr.gameObject);
			orders.Add (sr.sortingOrder);
		}
		Invoke("ReorderInLayers",0.05f);
        customizer.Init();
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
		lookAtTarget = World.Instance.scenesManager.cam.transform.localPosition;
        
    }
	int pathStep;
	//List<Point> points;
    List<Vector3> positions;
    public void MoveFromPath(List<Vector3> positions)
	{
        foreach (Vector3 pos in positions)
            print(pos);

		this.positions = positions;
		pathStep = 0;
		CharacterReachTile ();
	}
	void CharacterReachTile()
	{
		ReorderInLayers ();
		
		if (pathStep >= positions.Count) {
			Events.OnCharacterReachTile (this);
			Reset ();
			return;
		}
		pathStep++;
        print("CharacterReachTile  positions.Count: " + positions.Count + "   pathStep: " + pathStep + "  positions[pathStep-1]: " + positions[pathStep - 1]);
        Vector3 newPos = positions[pathStep-1];
		Move (newPos);

	}
	Vector3 newPos;
	public void Move(Vector3 _newPos)
	{
		Vector3 newScale = avatar.transform.localScale;
		if ((_newPos.x > transform.position.x && avatar.transform.localScale.x>0) || (_newPos.x < transform.position.x && avatar.transform.localScale.x<0)) {
			newScale.x *= -1;
		}
        avatar.transform.localScale = newScale;
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
