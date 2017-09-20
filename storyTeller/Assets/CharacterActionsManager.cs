using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionsManager : MonoBehaviour {

    public Settings.actions action;
    private Animation anim;
	Character character;

    void Awake()
    {
		character = GetComponent<Character> ();
        anim = GetComponent<Animation>();
    }

	public void Set(Settings.actions newAction) {
      //  if (newAction == action) return;

		character.Reset ();

        action = newAction;
        switch (newAction)
        {
            case Settings.actions.IDLE:
                Idle();
                break;
			case Settings.actions.HELLO:
				Hello();
				break;
			case Settings.actions.TURN:
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
		action = Settings.actions.IDLE;
		Idle ();
	}
	void Turn()
	{
		Vector3 scale= character.allBody.transform.localScale;
		scale.x *= -1;
		character.allBody.transform.localScale = scale;
	}
}
