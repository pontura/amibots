using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimelineScreen : MonoBehaviour {

	public Transform container;
	public TimeLineCharacter timelineCharacter;
	public GameObject panel;
	public TimelineKeyframe keyframe;

	void Start () {
		panel.SetActive (false);
	}
	public void Open () {
		panel.SetActive (true);
		Utils.RemoveAllChildsIn (container);
		Add ();
	}
	void Add()
	{
		foreach (CharacterData data in World.Instance.createdCharactersManager.GetActiveCharacters()) {
			TimeLineCharacter tlCharacter = Instantiate (timelineCharacter);
			tlCharacter.transform.SetParent (container);
			tlCharacter.Init (data);
			AddKeyframes (tlCharacter);
		}
	}
	void AddKeyframes (TimeLineCharacter timeLineCharacter)
	{
		List<KeyframeBase> keyframes = World.Instance.timeLine.GetkeyframesOfAvatar (timeLineCharacter.data.id);
		foreach (KeyframeBase keyframeBase in keyframes) {
			TimelineKeyframe newKeyframe = Instantiate (keyframe);
			newKeyframe.transform.SetParent (timeLineCharacter.container);
			newKeyframe.Init (keyframeBase);
		}
	}
	public void Close()
	{
		panel.SetActive (false);
	}
}
