using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISmallTimeline : MonoBehaviour {
	
	public GameObject panel;
	public Transform container;
	public KeyframeAsset keyFrameAsset;
	UITimeline uiTimeline;
	public List<KeyframeAsset> keyframes;
	public Slider slider;
	float duration;

	void Start () {
		panel.SetActive (false);
		Events.OnTimelineUpdated += OnTimelineUpdated;
		uiTimeline = GetComponent<UITimeline> ();
	}
	public void Init()
	{
		panel.SetActive (true);
	}

	void Update () {
		if (uiTimeline.state == UITimeline.states.PLAYING || uiTimeline.state == UITimeline.states.RECORDING) {
			slider.value = uiTimeline.timer / duration;
		}
	}
	void OnTimelineUpdated()
	{
		TimeLine timeline = World.Instance.timeLine;
		duration = timeline.GetDuration ();
		Utils.RemoveAllChildsIn (container);
		foreach (KeyframeBase k in timeline.keyframes) {
			AddKeyFrame (k.time / duration);
		}
	}
	void AddKeyFrame(float timer)
	{
		
		int pos = (int)(timer * 100);		
		KeyframeAsset newKeyFrameAsset = Instantiate (keyFrameAsset);
		newKeyFrameAsset.timer = timer;
		newKeyFrameAsset.transform.SetParent (container);
		newKeyFrameAsset.transform.localScale = Vector3.one;

		newKeyFrameAsset.GetComponent<RectTransform>().anchoredPosition = new Vector3 (pos, 0, 0);
		keyframes.Add (newKeyFrameAsset);
	}
	public void JumpTo(float value)
	{
		slider.value = value;
	}

}
