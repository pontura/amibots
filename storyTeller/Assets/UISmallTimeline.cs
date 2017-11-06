using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
		Events.OnUIButtonClicked += OnUIButtonClicked;
        Events.AddNewScene += AddNewScene;
        Events.NewSceneActive += NewSceneActive;
        uiTimeline = GetComponent<UITimeline> ();
	}
    void AddNewScene(int id, int bgID)
    {
        ResetTimeline();
    }
    void NewSceneActive(int id)
    {
        ResetTimeline();
        OnTimelineUpdated();
    }
    void ResetTimeline()
    {
        Utils.RemoveAllChildsIn(container);
        slider.value = 0;
    }
    void OnUIButtonClicked(UIButton uiButton)
	{
		if (uiButton.type == UIButton.types.REW_TO_CHECKPOINT)
			JumpToCheckpoint (true);
		else if (uiButton.type == UIButton.types.FAST_TO_CHECKPOINT)
			JumpToCheckpoint (false);
	}
	public void OnPointerUp()
	{
		uiTimeline.JumptTo (slider.value*duration);
	}
	public void Init()
	{
		panel.SetActive (true);
	}

	public void UpdatedByUITimeline () {
		slider.value = uiTimeline.timer / duration;
	}
	void OnTimelineUpdated()
	{
		TimeLine timeline = World.Instance.timeLine;
		duration = timeline.GetDuration ();
		Utils.RemoveAllChildsIn (container);
		foreach (KeyframeBase k in timeline.GetActiveScenesTimeline().keyframes) {
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
	void JumpToCheckpoint(bool rew)
	{
		TimeLine timeline = World.Instance.timeLine;
		float currentTime = slider.value * duration;
		float timer = 0;
		if (rew) {		
			timer = 0;	
			foreach (KeyframeBase k in timeline.GetActiveScenesTimeline().keyframes) {
				if (k.time < currentTime && k.time>timer)
					timer = k.time;
			}
		} else {
			timer = duration;	
			foreach (KeyframeBase k in timeline.GetActiveScenesTimeline().keyframes) {
				if (k.time > currentTime && k.time<timer) {
					timer = k.time;
					continue;
				}
			}
		}
	//	print (currentTime + "          Timer: " + timer);
		JumpTo(timer/duration);
		uiTimeline.JumptTo (timer);

	}

}
