using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineKeyframe : MonoBehaviour {

	public Text field;
	KeyframeBase data;
	int separation = 10;

	public void Init(KeyframeBase data) {
		this.data = data;
		field.text = data.time.ToString();
		SetPosition ();
	}
	void SetPosition()
	{
		transform.localPosition = new Vector2 (data.time * separation, 0);
	}
	public void Clicked()
	{
		
	}
}
