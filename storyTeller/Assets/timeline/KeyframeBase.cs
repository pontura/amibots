using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class KeyframeBase {
	public float time;
	public Vector3 pos;
	public Vector3 moveTo;
	public KeyframeAvatar avatar;
}