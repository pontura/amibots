using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionsManager : MonoBehaviour {

    public string action;
    private Animation anim;
	Character character;

    void Awake()
    {
		character = GetComponent<Character> ();
        anim = GetComponent<Animation>();
		action = "IDLE";
    }
	void Start()
	{
		ResetAnim ();
	}
	public void Set(string newAction) {
		character.Reset ();

        action = newAction;
        switch (newAction)
        {
            case "IDLE":
                Idle();
                break;
			case "HELLO":
				Hello();
				break;
			case "TURN":
				Turn();
				break;
			default:
				Debug.Log ("No action for: " + newAction);
				break;
        }
	}
    public void Idle()
    {
		anim.Play("idle");
    }
	public void Walk()
    {
		anim.Play("walk");
    }
	void Hello()
	{
		anim.Play("hello");
		Invoke ("ResetAnim", 1);
	}
	void ResetAnim()
	{
		action = "IDLE";
		Idle ();
	}
	void Turn()
	{
		Vector3 scale= character.allBody.transform.localScale;
		scale.x *= -1;
		character.allBody.transform.localScale = scale;
	}
}
