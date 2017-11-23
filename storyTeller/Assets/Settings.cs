using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	public int totalScenes;

    public enum actions
    {
        IDLE,
		WOW,
		LOL,
        GRR,
        SOB
    }
	public enum expressions
	{
        norm,
        angry,
        confused,
        funny,
        haha,
        happy,        
        oh,
        sad,
		what,
		zzz
	}
}
