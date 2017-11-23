using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionsManager : MonoBehaviour {

    public string action;
    public Animator anim;
    public Animator expressionsAnim;
    Character character;

    void Awake()
    {
		character = GetComponent<Character> ();
        
        action = "IDLE";
    }
	public void Init()
	{
        anim = character.avatar.actionsAnim;
        expressionsAnim = character.avatar.expressionsAnim;
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
			case "LOL":
				Lol();
				break;
			case "GRR":
				Grr();
				break;
            case "SOB":
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
		PlayAnim("idle");
    }
	public void Walk()
    {
		if(Random.Range(0,10)<5)
			PlayAnim("walk");
		else
			PlayAnim("walk");
    }
	public void Lol()
	{
		PlayAnim("lol");
        expressionsAnim.Play("haha");
    }
	public void Grr()
	{
		PlayAnim("grr");
        expressionsAnim.Play("angry");
    }
    public void Sob()
    {
        PlayAnim("sob");
        expressionsAnim.Play("sad");
    }
    public void Wow()
	{
		PlayAnim("wow");
        expressionsAnim.Play("oh");
    }
	void ResetAnim()
	{
		PlayAnim("IDLE");
		action = "IDLE";
		Idle ();
	}
    public void SetExpression( string value)
    {
        print("SetExpression " + value);
        expressionsAnim.Play(value);
        //action = "IDLE";
       // Idle();
    }
    void Turn()
	{
		Vector3 scale= character.avatar.transform.localScale;
		scale.x *= -1;
		character.avatar.transform.localScale = scale;
	}
	void PlayAnim(string animName)
	{
		anim.Play(animName);
	}
}
