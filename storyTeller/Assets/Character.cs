using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class Character : MonoBehaviour {

	public float speed;
    public int id;
    public bool isEditorCharacter;

    public GameObject allBody;

    public GameObject body;
    public GameObject hips;
    public GameObject head;

	public GameObject foot_right;
	public GameObject foot_left;

	public GameObject hand_right;
	public GameObject hand_left;

	Animation anim;
    public bool falled;

   
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

	void Start () {
        anim = GetComponent<Animation> ();
        actions = GetComponent<CharacterActionsManager>();
		chatLine = GetComponent<CharacterChatLine> ();
		customizer = GetComponent<CharacterCustomizer> ();
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
        lookAtTarget = World.Instance.camera_in_scene.transform.localPosition;
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
	//	print ("CharacterReachTile  points.Count: " + points.Count + "   pathStep: " + pathStep);
		if (pathStep >= points.Count) {
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
    GameObject GetBodyPartByClassName(string className)
    {
        switch (className)
        {
            case "right foot":
                return foot_right;
            case "left foot":
                return foot_left;
            case "right hand":
                return hand_right;
            case "left hand":
                return hand_left;
            case "hips":
                return hips;
			case "head":
				return head;
        }
        return null;
    }

}
