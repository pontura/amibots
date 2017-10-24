using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionsManager : MonoBehaviour {

    public string action;
	public Animator anim;
	Character character;

    void Awake()
    {
		character = GetComponent<Character> ();
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
			case "LOL":
				Lol();
				break;
			case "GRR":
				Grr();
				break;
			case "WOW":
				Wow();
				break;
			default:
				Debug.Log ("No action for: " + newAction);
				break;
        }
	}
    public void Idle()
    {
		anim.Play("idle1");
    }
	public void Walk()
    {
		if(Random.Range(0,10)<5)
			anim.Play("walk1");
		else
			anim.Play("walk2");
    }
	public void Lol()
	{
		anim.Play("lol");
	}
	public void Grr()
	{
		anim.Play("grrr");
	}
	public void Wow()
	{
		anim.Play("wow");
	}
	void Hello()
	{
		anim.Play("wow");
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
