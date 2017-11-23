using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStates : MonoBehaviour {

    public states state;

    public enum states
    {
        CHARACTERS_EDITOR,
        SCENE_EDITOR
    }
	public void Change(states state) {
        this.state = state;
        if (state == states.CHARACTERS_EDITOR)
            Events.OnSetColliders(false);
        else
            Events.OnSetColliders(true);
       
	}
}
