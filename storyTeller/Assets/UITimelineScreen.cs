using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimelineScreen : MonoBehaviour {

	public Transform container;
	public TimeLineCharacter timelineCharacter;
	public GameObject panel;
	public TimelineKeyframe keyframe;
	public TimelineMarker timelineMarker_to_instantiate;
	public TimelineMarker timelineMarker;
	public Vector2 offset = new Vector3(20,0);
	public float separation = 100;
	public float _y_separation = -69;
	List<TimeLineCharacter> all;
	List<TimelineKeyframe> timelineKeyframes;
	public TimeLine timeline;
	public Text field;
	KeyframeBase activeKeyframe;

	void Start () {
		panel.SetActive (false);
	}
	public void Open () {
		World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(true);
		panel.SetActive (true);
		Utils.RemoveAllChildsIn (container);

		timelineMarker = Instantiate (timelineMarker_to_instantiate);

		timelineMarker.transform.SetParent (container);
		timelineMarker.transform.localScale = Vector2.one;
		timelineMarker.SetX(0);

		all = new List<TimeLineCharacter> ();
		timelineKeyframes = new List<TimelineKeyframe> ();

		Add ();
	}
	void Add()
	{
		int id = 0;
		foreach (CharacterData data in World.Instance.createdCharactersManager.GetActiveCharacters()) {
			TimeLineCharacter tlCharacter = Instantiate (timelineCharacter);
			tlCharacter.transform.SetParent (container);
			tlCharacter.transform.localScale = Vector2.one;
			tlCharacter.Init (data);
			tlCharacter.transform.localPosition = new Vector2 (0,_y_separation*id);
			AddKeyframes (tlCharacter);
			all.Add (tlCharacter);
			id++;
		}
	}
	void AddKeyframes (TimeLineCharacter timeLineCharacter)
	{
		
		List<KeyframeBase> keyframes = World.Instance.timeLine.GetkeyframesOfAvatar (timeLineCharacter.data.id);
		foreach (KeyframeBase keyframeBase in keyframes) {
			TimelineKeyframe newKeyframe = Instantiate (keyframe);
			newKeyframe.transform.SetParent (timeLineCharacter.transform);
			newKeyframe.Init (this, keyframeBase, offset, separation);
			Resize (timeLineCharacter, newKeyframe.transform.localPosition.x);
			timelineKeyframes.Add (newKeyframe);
		}
	}
	void Resize(TimeLineCharacter timeLineCharacter, float _x)
	{
		timeLineCharacter.GetComponent<RectTransform> ().sizeDelta = new Vector2 (_x,timeLineCharacter.GetComponent<RectTransform> ().sizeDelta.y);
		if(container.GetComponent<RectTransform> ().sizeDelta.x < _x)
			container.GetComponent<RectTransform> ().sizeDelta = new Vector2 (_x, container.localScale.y);
	}
	public void Close()
	{
		World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(false);
		panel.SetActive (false);
	}

	public void ActiveKeyframe(KeyframeBase data)
	{		
		foreach(TimelineKeyframe tlk in timelineKeyframes)
		{
			tlk.Reset ();
		}
		this.activeKeyframe = activeKeyframe;
		Vector2 pos = offset + new Vector2 ((data.time * separation), 0);
		timelineMarker.SetX (pos.x);
		timeline.JumpTo (data.time);
		UpdateField ();

	}
	void UpdateField()
	{
		int minutes = Mathf.FloorToInt(timeline.timer / 60) % 60;
		int seconds = Mathf.FloorToInt(timeline.timer - minutes * 60) % 60;
		int milliseconds = Mathf.FloorToInt((timeline.timer - (minutes) * 60) * 60) % 100;
		field.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
	}
	public void DeleteKeyframe(TimelineKeyframe keyframe, int characterID, float _timer)
	{
		timeline.DeleteKeyframe (characterID, _timer);
		timelineKeyframes.Remove (keyframe);
	}
}
