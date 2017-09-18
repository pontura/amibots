using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool isEditorCharacter;
    public GameObject pivot;
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

    public states state;
    public Vector3 lookAtTarget;

    public enum states
	{
		IDLE,
		FALL,
	}

	void Start () {
        anim = GetComponent<Animation> ();
        Events.ClickedOn += ClickedOn;
    }
    void OnDestroy()
    {
        Events.ClickedOn -= ClickedOn;
    }
    void ClickedOn(Vector3 pos)
    {
        lookAtTarget = pos;
    }

    void CharacterFall(string _anim)
	{
        falled = true;
        state = states.FALL;
		anim.Play (_anim);
	}
	
    float XOffset;
	public void Reset()
	{
      
        //anim.Play ("idle");
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
