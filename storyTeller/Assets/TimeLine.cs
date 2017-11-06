using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeLine : MonoBehaviour {
	
	public float timer;
	public List<ScenesTimeline> scenesTimeline;

	[Serializable]
	public class ScenesTimeline
	{
		public int id;
		public List<KeyframeBase> keyframes;
	}
	ScenesManager scenesManager;
	CharactersManager charactersManager;
	public UITimeline uiTimeline;
    public int activeSceneID;
	float fullDuration;

	void Start () {
		scenesManager = GetComponent<ScenesManager> ();
		scenesTimeline = new List<ScenesTimeline> ();
		charactersManager = World.Instance.charactersManager;
		Events.OnCharacterReachTile += OnCharacterReachTile;
		Events.AddCharacter += AddCharacter;
		Events.AddKeyFrameNewCharacter += AddKeyFrameNewCharacter;
		Events.OnRecording += OnRecording;
		Events.OnPlaying += OnPlaying;
		Events.AddKeyFrameMove += AddKeyFrameMove;
		Events.AddKeyFrameAction += AddKeyFrameAction;
		Events.AddKeyFrameExpression += AddKeyFrameExpression;
		Events.OnCharacterSay += OnCharacterSay;
		Events.AddNewScene += AddNewScene;
        Events.OnActivateScene += OnActivateScene;

    }
	void AddCharacter(int id)
	{
		print ("AddCharacter");
	}
	public ScenesTimeline GetActiveScenesTimeline()
	{
		return scenesTimeline [scenesManager.sceneActive.id];
	}
	void AddNewScene(int id, int backgroundID)
	{
        timer = 0;
        ScenesTimeline st = new ScenesTimeline ();
		st.keyframes = new List<KeyframeBase> ();
		st.id = id;
		scenesTimeline.Add (st);
	}
	void OnCharacterReachTile(Character character)
	{
		if (uiTimeline.state != UITimeline.states.RECORDING)
			return;
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe == null)
			return;
		GetActiveScenesTimeline().keyframes.Add (keyframe);
	}
	void AddKeyFrameNewCharacter(Character character)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe == null)
			return;
		GetActiveScenesTimeline().keyframes.Add (keyframe);
	}
	void OnRecording(bool isRecording)
	{			
		print ("OnRecording " + isRecording);	
		if (isRecording) {
			
			int selectedAvatarID = charactersManager.selectedCharacter.id;
			RemoveLaterKeyFramesFor (selectedAvatarID);

			foreach (Character character in World.Instance.scenesManager.sceneActive.characters) {
				KeyframeBase keyframe = GetNewKeyframeAvatar (character);
				if (keyframe == null)
					return;
				GetActiveScenesTimeline ().keyframes.Add (keyframe);
			}
		}
		Events.OnTimelineUpdated ();
	}
	void AddKeyFrameMove(Character character, Vector3 moveTo)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe == null) return;
		keyframe.moveTo = moveTo;
		GetActiveScenesTimeline().keyframes.Add (keyframe);
	}
	void AddKeyFrameAction(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.action = value;
		GetActiveScenesTimeline().keyframes.Add (keyframe);
	}
	void AddKeyFrameExpression(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.expression = value;
		GetActiveScenesTimeline().keyframes.Add (keyframe);
	}
	void OnCharacterSay(int characterID, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (World.Instance.charactersManager.GetCharacter(characterID));
		if (keyframe.avatar == null) return;
		keyframe.avatar.chat = value;
		GetActiveScenesTimeline().keyframes.Add (keyframe);
	}
	KeyframeBase GetNewKeyframeAvatar(Character character)
	{
		//RemoveLaterKeyFramesFor (character.id);
		KeyframeBase keyframe = new KeyframeBase ();
		keyframe.time = uiTimeline.timer;

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
		if (isPlaying) {
			fullDuration = GetDuration ();		
			timer = uiTimeline.timer;	
			foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes)
				keyFrame.played = false;
		}
	}

	void Update()
	{
		if (uiTimeline.state == UITimeline.states.PLAY_ALL || 
			uiTimeline.state == UITimeline.states.PLAYING  || 
			uiTimeline.state == UITimeline.states.RECORDING) {
			timer += Time.deltaTime;
            ScenesTimeline scenesTimeline = GetActiveScenesTimeline();
            if (activeSceneID != scenesTimeline.id)
            {
                Events.OnActivateScene(scenesTimeline.id);
                activeSceneID = scenesTimeline.id;
            }

            foreach (KeyframeBase keyFrame in scenesTimeline.keyframes) {
				if (keyFrame.time <= timer && keyFrame.played == false) {
					SetActiveKeyFrame (keyFrame);
					keyFrame.played = true;
				}
			}
			if (uiTimeline.state == UITimeline.states.PLAYING && timer >= fullDuration) {
				Events.OnPlaying (false);
			}
		}
	}
    void OnActivateScene(int id)
    {
        timer = 0;
        activeSceneID = id;
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
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.avatar != null) {
				KeyframeAvatar keyframeAvatar = keyFrame.avatar;
				if (keyframeAvatar.avatarID == avatarID && keyFrame.time >= uiTimeline.timer)
					keyframesToDelete.Add (keyFrame);
			}
		}
		foreach (KeyframeBase k in keyframesToDelete)
			GetActiveScenesTimeline().keyframes.Remove (k);
	}
	public float GetLastRecordedKeyFrame(int avatarID)
	{
		float keyframeTime = 0;
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.avatar != null) {
				KeyframeAvatar keyframeAvatar = keyFrame.avatar;
				if (keyframeAvatar.avatarID == avatarID)
					keyframeTime = keyFrame.time;
			}
		}
		return keyframeTime;
	}
	public void RewindAll()
	{
		World.Instance.charactersManager.RestartScene ();
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if(keyFrame.time == 0 && keyFrame.moveTo == Vector3.zero)
				SetActiveKeyFrame (keyFrame);
		}
	}
	public void FastForward()
	{
		foreach (Character character in World.Instance.scenesManager.sceneActive.characters) {
			float _duration = GetDuration ();
			charactersManager.PositionateCharacter (character.id, GetLastPositionInTime (character.id, _duration));
		}
	}
	public void JumpTo(float _timer)
	{
        int newSceneActiveID = 0;
        foreach (ScenesTimeline s in scenesTimeline)
        {
            foreach (KeyframeBase keyFrame in s.keyframes)
            {
                if (keyFrame.time <= _timer)
                    newSceneActiveID = s.id;
            }
        }
       
        if (activeSceneID != newSceneActiveID)
        {
            print("CAMBIA de " + activeSceneID + " a " + newSceneActiveID);
            activeSceneID = newSceneActiveID;
            Events.OnActivateScene(activeSceneID);
           
        }

        foreach (Character character in World.Instance.scenesManager.sceneActive.characters) {
			charactersManager.PositionateCharacter (character.id, GetLastPositionInTime (character.id, _timer));
		}
	}
	public float GetDuration()
	{
		float _duration = 0;
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.time > _duration)
				_duration = keyFrame.time;
		}
		return _duration;
	}
	Vector3 GetLastPositionInTime(int characterID, float time)
	{
		Vector3 pos = Vector3.zero;
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.avatar.avatarID == characterID && keyFrame.time<=time)
				pos = keyFrame.pos;
		}
		return pos;
	}
	public void PlayAll()
	{
		timer = 0;
		activeSceneID = 0;
	}
}
