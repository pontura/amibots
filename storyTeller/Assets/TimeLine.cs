﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour {
	
	public int timer;
	public List<KeyframeBase> keyframes;
	public CharactersManager charactersManager;
	public UITimeline uiTimeline;

	void Start () {
		Events.OnRecording += OnRecording;
		Events.OnPlaying += OnPlaying;
		Events.AddKeyFrameMove += AddKeyFrameMove;
		Events.AddKeyFrameAction += AddKeyFrameAction;
		Events.AddKeyFrameExpression += AddKeyFrameExpression;
		Events.OnCharacterSay += OnCharacterSay;
	}

	void OnRecording(bool isRecording)
	{
		if (isRecording) {
			foreach(Character character in World.Instance.charactersManager.characters)
				RemoveLaterKeyFramesFor (character.id);
		}

		foreach (Character character in World.Instance.charactersManager.characters) {
			KeyframeBase keyframe = GetNewKeyframeAvatar (character);
			if (keyframe == null)
				return;
			keyframes.Add (keyframe);
		}
	}
	void AddKeyFrameMove(Character character, Vector3 moveTo)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe == null) return;
		keyframe.moveTo = moveTo;
		keyframes.Add (keyframe);
	}
	void AddKeyFrameAction(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.action = value;
		keyframes.Add (keyframe);
	}
	void AddKeyFrameExpression(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.expression = value;
		keyframes.Add (keyframe);
	}
	void OnCharacterSay(int characterID, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (World.Instance.charactersManager.GetCharacter(characterID));
		if (keyframe.avatar == null) return;
		keyframe.avatar.chat = value;
		keyframes.Add (keyframe);
	}
	KeyframeBase GetNewKeyframeAvatar(Character character)
	{
		RemoveLaterKeyFramesFor (character.id);
		KeyframeBase keyframe = new KeyframeBase ();
		keyframe.time = (int)uiTimeline.timer;

		KeyframeAvatar keyframeAvatar = new KeyframeAvatar ();
		keyframeAvatar.avatarID = character.id;
		keyframeAvatar.action = character.actions.action.ToString();
		keyframeAvatar.expression = character.customizer.value;
		keyframe.avatar = keyframeAvatar;

		keyframe.pos = character.transform.position;
		return keyframe;
	}

	void OnPlaying(bool isPlaying) 
	{
		timer = -1;
	}

	void Update()
	{
		if (uiTimeline.state == UITimeline.states.PLAYING) {
			int uiTimelineTimer = (int)uiTimeline.timer;
			if (uiTimelineTimer != timer) {
				NewSecond ();
				timer = uiTimelineTimer;
			}
		}
	}
	void NewSecond()
	{
		foreach (KeyframeBase keyFrame in keyframes) {
			if (keyFrame.time == timer) {
				SetActiveKeyFrame (keyFrame);
			}
		}
	}
	void SetActiveKeyFrame(KeyframeBase keyFrame)
	{		
		if (keyFrame.avatar != null) {
			KeyframeAvatar keyframeAvatar = keyFrame.avatar;
			int avatarID = keyFrame.avatar.avatarID;
			string action = keyFrame.avatar.action;
			string expression = keyFrame.avatar.expression;
			string chat = keyFrame.avatar.chat;
			Vector3 moveTo = keyFrame.moveTo;
			Vector3 pos = keyFrame.pos;

			charactersManager.PositionateCharacter (avatarID, pos);

			charactersManager.CharacterAction (avatarID, action);
			charactersManager.ChangeExpression (avatarID, expression);

			if (chat != null && chat.Length >0) {
				uiTimeline.GetComponent<UIChatManager>().OnCharacterSay(avatarID, chat);
			}
			
			if(moveTo != Vector3.zero)
				charactersManager.MoveCharacter (avatarID, moveTo);
		}
	}
	void RemoveLaterKeyFramesFor(int avatarID)
	{
		List<KeyframeBase> keyframesToDelete = new List<KeyframeBase>();
		foreach (KeyframeBase keyFrame in keyframes) {
			if (keyFrame.avatar != null) {
				KeyframeAvatar keyframeAvatar = keyFrame.avatar;
				if (keyframeAvatar.avatarID == avatarID && keyFrame.time >= uiTimeline.timer)
					keyframesToDelete.Add (keyFrame);
			}
		}
		foreach (KeyframeBase k in keyframesToDelete)
			keyframes.Remove (k);
	}
	public int GetLastRecordedKeyFrame(int avatarID)
	{
		int keyframeTime = 0;
		foreach (KeyframeBase keyFrame in keyframes) {
			if (keyFrame.avatar != null) {
				KeyframeAvatar keyframeAvatar = keyFrame.avatar;
				if (keyframeAvatar.avatarID == avatarID)
					keyframeTime = (int)keyFrame.time;
			}
		}
		return keyframeTime;
	}
	public void RewindAll()
	{
		World.Instance.charactersManager.RestartScene ();
		foreach (KeyframeBase keyFrame in keyframes) {
			if(keyFrame.time == 0 && keyFrame.moveTo == Vector3.zero)
				SetActiveKeyFrame (keyFrame);
		}
	}
	public void FastForward()
	{
		foreach (KeyframeBase keyFrame in keyframes) {
			if(keyFrame.time == 0)
			{
				//SetActiveKeyFrame (keyFrame);
			}
		}
	}
}