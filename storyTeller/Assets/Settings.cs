using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	public int totalScenes;

    public enum actions
    {
        IDLE,
		WOW,
		LOL
    }
	public enum expressions
	{
        angry,
        confused,
        funny,
        happy,
        sad,
		what,
		zzz
	}
}
