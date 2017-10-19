using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeline : MonoBehaviour {

	public states state;
	public enum states
	{
		STOPPED,
		RECORDING,
		PLAYING
	}

	public Button RecButton;
	public Button PlayButton;
	public Button RewButton;
	public Button FFButton;

	public Color colorStopped;
	public Color colorRec;
	public Color colorPlaying;

	public Image bgImage;
	public GameObject panel;
	public Text timeField;
	public Text buttonField;
	public Text buttonPlayField;
	public float timer = 0;
	float timelineDuration;
	public UISmallTimeline uiSmallTimeline;

	void Start () {
		uiSmallTimeline = GetComponent<UISmallTimeline> ();
		PlayButton.interactable = false;
		RewButton.interactable = false;
		FFButton.interactable = false;

		panel.SetActive (false);
		SetTimerField ();
		Events.OnUIButtonClicked += OnUIButtonClicked;
		Events.AddCharacter += AddCharacter;
	}
	void OnDestroy () {
		SetTimerField ();
		Events.OnUIButtonClicked -= OnUIButtonClicked;
		Events.AddCharacter -= AddCharacter;
	}
	void AddCharacter(int id)
	{
		panel.SetActive (true);
	}
	void OnUIButtonClicked(UIButton uiButton)
	{
		if (uiButton.type == UIButton.types.REC_TOGGLE)
			Toggle ();
		else if (uiButton.type == UIButton.types.PLAY_TOGGLE)
			PlayToggle ();
		else if (uiButton.type == UIButton.types.REWIND) {
			timer = 0;
			SetTimerField ();
			World.Instance.timeLine.RewindAll();
			uiSmallTimeline.JumpTo (0);
		} else if (uiButton.type == UIButton.types.FAST_FORWARD) {
			World.Instance.timeLine.FastForward();
			timer = World.Instance.timeLine.GetLastRecordedKeyFrame(World.Instance.charactersManager.selectedCharacter.id);
			uiSmallTimeline.JumpTo (1);
		}
	}
	public void Toggle()
	{
		if (state != states.RECORDING)
			state = states.RECORDING;
		else
			state = states.STOPPED;
		
		if (state == states.RECORDING) {
			PlayButton.interactable = false;
			RewButton.interactable = false;
			FFButton.interactable = false;

			buttonField.text = "STOP";
			bgImage.color = colorRec;
			Events.OnRecording (true);
		} else {
			uiSmallTimeline.Init ();
			PlayButton.interactable = true;
			RewButton.interactable = true;
			FFButton.interactable = true;

			buttonField.text = "REC";
			bgImage.color = colorStopped;
			Events.OnRecording (false);
		}
		SetTimerField ();
	}
	public void PlayToggle()
	{		
		if (state != states.PLAYING)
			state = states.PLAYING;
		else
			state = states.STOPPED;
		
		if (state == states.PLAYING) {
			bgImage.color = colorPlaying;
			timelineDuration = World.Instance.timeLine.GetDuration();
			RecButton.interactable = false;
			buttonPlayField.text = "STOP";
			Events.OnPlaying (true);
		} else {
			bgImage.color = colorStopped;
			RecButton.interactable = true;
			buttonPlayField.text = "PLAY";
			Events.OnPlaying (false);
		} 
		SetTimerField ();
	}
	void Update () {
		if (state == states.PLAYING && timer>timelineDuration) {
			PlayToggle ();
		}
		if (state == states.STOPPED)
			return;
		timer += Time.deltaTime;
		SetTimerField ();
	}
	void SetTimerField()
	{
		int minutes = Mathf.FloorToInt(timer/60)%60;
		int seconds = Mathf.FloorToInt(timer-minutes*60)%60;
		int milliseconds = Mathf.FloorToInt( (timer-(minutes)*60) *60)%100;
		timeField.text = string.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
	}

}
