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
		PLAYING,
		PLAY_ALL
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
        Events.AddNewScene += AddNewScene;
        Events.OnActivateScene += OnActivateScene;
		Events.OnPlaying += OnPlaying;
    }
	void OnDestroy () {
		SetTimerField ();
		Events.OnUIButtonClicked -= OnUIButtonClicked;
		Events.AddCharacter -= AddCharacter;
        Events.AddNewScene -= AddNewScene;
        Events.OnActivateScene -= OnActivateScene;
		Events.OnPlaying -= OnPlaying;
    }
	void OnPlaying(bool isPlaying)
	{
		if (!isPlaying) {
			bgImage.color = colorStopped;
			RecButton.interactable = true;
			buttonPlayField.text = "PLAY";
			state = states.STOPPED;
		} else if(state != states.PLAY_ALL){
			bgImage.color = colorPlaying;
			//timelineDuration = World.Instance.timeLine.GetDuration();
			RecButton.interactable = false;
			buttonPlayField.text = "STOP";
			state = states.PLAYING;
		}
		SetTimerField ();
	}
    void AddNewScene(int sceneID, int bg)
    {
        Invoke("ResetAll", 0.1f);
    }
    void OnActivateScene(int sceneID)
    {
        print("OnActivateScene " + sceneID);
        Invoke("ResetAll", 0.1f);
    }
    void ResetAll()
    {
        timer = 0;
        SetTimerField();
        World.Instance.timeLine.RewindAll();
        uiSmallTimeline.JumpTo(0);
    }

    void AddCharacter(CharacterData data)
	{
		panel.SetActive (true);
	}
	void OnUIButtonClicked(UIButton uiButton)
	{
        if (uiButton.type == UIButton.types.REC_TOGGLE)
            Toggle();
        else if (uiButton.type == UIButton.types.PLAY_TOGGLE)
        {
            if (timer >= World.Instance.timeLine.GetDuration()-1)
            {
                timer = 0;
                SetTimerField();
                World.Instance.timeLine.RewindAll();
                uiSmallTimeline.JumpTo(0);
            }
            PlayToggle();
        }
        else if (uiButton.type == UIButton.types.REWIND)
        {
            timer = 0;
            SetTimerField();
            World.Instance.timeLine.RewindAll();
            uiSmallTimeline.JumpTo(0);
        }
        else if (uiButton.type == UIButton.types.FAST_FORWARD)
        {
            World.Instance.timeLine.FastForward();
            timer = World.Instance.timeLine.GetLastRecordedKeyFrame(World.Instance.charactersManager.selectedCharacter.data.id);
            uiSmallTimeline.JumpTo(1);
        }
	}
	public void JumptTo(float value)
	{
		timer = value;
		World.Instance.timeLine.JumpTo(timer);
		SetTimerField ();
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
            GetComponent<UIRecScreen>().SetState(true);
        } else {
			uiSmallTimeline.Init ();
			PlayButton.interactable = true;
			RewButton.interactable = true;
			FFButton.interactable = true;

			buttonField.text = "REC";
			bgImage.color = colorStopped;
			Events.OnRecording (false);
            GetComponent<UIRecScreen>().SetState(false);
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
          //  timer -= 0.1f;

            Events.OnPlaying (true);
		} else {
			Events.OnPlaying (false);
		} 
	}
	void Update () {
		if (state == states.STOPPED)
			return;
		uiSmallTimeline.UpdatedByUITimeline ();
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
	public void PlayAllClicked()
	{
        Events.OnActivateScene(0);
        Events.OnPlaying(true);
        GetComponent<UIPreview>().MaskOn();
        Invoke("PlayAllClickedDelayed", 0.1f);
	}
    void PlayAllClickedDelayed()
    {
        state = states.PLAY_ALL;
        print("AAA");
        World.Instance.timeLine.PlayAll();
        GetComponent<UIPreview>().Init();
        GetComponent<UIPreview>().HideButton();
    }
	public void StopPlaying()
	{
		state = states.STOPPED;
	}

}
