using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimelineKeyframe : MonoBehaviour {

	public Text field;
	KeyframeBase data;

	public Image image;
	bool selected;
	public GameObject deleteIcon;

	UITimelineScreen uiTimelineScreen;

	public void Init(UITimelineScreen _uiTimelineScreen, KeyframeBase data, Vector2 offset, float separation ) {
		deleteIcon.SetActive (false);
		this.uiTimelineScreen = _uiTimelineScreen;
		this.data = data;
		//field.text = data.time.ToString();
		transform.localPosition = offset + new Vector2 ((data.time * separation), 0);
	}
	public void Clicked()
	{
		if (selected) {
			uiTimelineScreen.DeleteKeyframe (this, data.avatar.avatarID, data.time);
			//Destroy (gameObject);
		} else {
			uiTimelineScreen.ActiveKeyframe (data);
			selected = true;
			deleteIcon.SetActive (true);
		}
	}
	public void Reset()
	{
		selected = false;
		deleteIcon.SetActive (false);
	}
}
