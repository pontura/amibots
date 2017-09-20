using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionsManager : MonoBehaviour {

    public Settings.actions action;
    private Animation anim;

    void Awake()
    {
        anim = GetComponent<Animation>();
    }

	public void Set(Settings.actions newAction) {
        if (newAction == action) return;
        action = newAction;
        switch (newAction)
        {
            case Settings.actions.IDLE:
                Idle();
                break;
            case Settings.actions.WALK:
                Walk();
                break;
        }
	}
    void Idle()
    {
        anim.Play("idle");
    }
    void Walk()
    {
        anim.Play("walk");
    }
}
