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
        print("Init: ");
        ResetAnim ();
	}
	public void Set(string newAction) {

        print("newAction: " + newAction);
		//character.Reset ();

        action = newAction;
        switch (newAction)
        {
			case "WALK":
				Walk();
				break;
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
    void Idle()
    {
		PlayAnim("idle");
    }
	void Walk()
    {
		if(Random.Range(0,10)<5)
			PlayAnim("walk");
		else
			PlayAnim("walk");
    }
	void Lol()
	{
		PlayAnim("lol");
        expressionsAnim.Play("haha");
    }
	void Grr()
	{
		PlayAnim("grr");
        expressionsAnim.Play("angry");
    }
    void Sob()
    {
        PlayAnim("sob");
        expressionsAnim.Play("sad");
    }
    void Wow()
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
       
		if (value.Length < 2)
			return;

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
