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
        public KeyframeScreenTitle screenTitle;
	}
	ScenesManager scenesManager;
	CharactersManager charactersManager;
	public UITimeline uiTimeline;
    public int activeSceneID;
    public float fullDuration;
    public ScenesTimeline activeScenesTimeline;

    void Start () {
		scenesManager = GetComponent<ScenesManager> ();
		scenesTimeline = new List<ScenesTimeline> ();
		charactersManager = World.Instance.charactersManager;
		Events.OnCharacterReachTile += OnCharacterReachTile;
		Events.AddKeyFrameNewCharacter += AddKeyFrameNewCharacter;
        Events.RefreshKeyframe += RefreshKeyframe;
        Events.OnRecording += OnRecording;
		Events.OnPlaying += OnPlaying;
		Events.AddKeyFrameMove += AddKeyFrameMove;
		Events.AddKeyFrameScreenTitle += AddKeyFrameScreenTitle;
		Events.AddKeyFrameAction += AddKeyFrameAction;
		Events.AddKeyFrameExpression += AddKeyFrameExpression;
		Events.OnCharacterSay += OnCharacterSay;
		Events.AddNewScene += AddNewScene;
        Events.OnActivateScene += OnActivateScene;

    }
	public ScenesTimeline GetActiveScenesTimeline()
	{
		return scenesTimeline [scenesManager.sceneActive.id];
	}
    void RefreshKeyframe()
    {

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
        KeyframeBase keyframe = GetNewKeyframeAvatar(character);
        if (timer == 0 && uiTimeline.state == UITimeline.states.STOPPED)
        {
            if (keyframe != null)
            {
                keyframe.pos = character.transform.position;
                RemoveLaterKeyFramesFor(character.data.id);
                GetActiveScenesTimeline().keyframes.Add(keyframe);
            }
        }      
		if (uiTimeline.state != UITimeline.states.RECORDING)
			return;
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
        activeScenesTimeline = GetActiveScenesTimeline();
        int selectedAvatarID = charactersManager.selectedCharacter.data.id;

        if (isRecording) {            
			RemoveLaterKeyFramesFor (selectedAvatarID);			
		}
        foreach (Character character in World.Instance.scenesManager.sceneActive.characters)
        {
            KeyframeBase keyframe = GetNewKeyframeAvatar(character);
            if (keyframe == null)
                return;
            activeScenesTimeline.keyframes.Add(keyframe);
        }
        Events.OnTimelineUpdated ();
	}
	void AddKeyFrameScreenTitle(string title, float time)
	{
		ScenesTimeline sceneTimeline = new ScenesTimeline();
        sceneTimeline.screenTitle = new KeyframeScreenTitle ();
        sceneTimeline.screenTitle.title = title;
        sceneTimeline.keyframes = new List<KeyframeBase>();
        scenesTimeline.Add (sceneTimeline);
	}
	void AddKeyFrameMove(Character character, Vector3 moveTo)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe == null) return;
		keyframe.moveTo = moveTo;
        activeScenesTimeline.keyframes.Add (keyframe);
	}
	void AddKeyFrameAction(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.action = value;
        activeScenesTimeline.keyframes.Add (keyframe);
	}
	void AddKeyFrameExpression(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.expression = value;
        activeScenesTimeline.keyframes.Add (keyframe);
	}
	void OnCharacterSay(int characterID, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (World.Instance.charactersManager.GetCharacter(characterID));
		if (keyframe.avatar == null) return;
		keyframe.avatar.chat = value;
        activeScenesTimeline.keyframes.Add (keyframe);
	}
	KeyframeBase GetNewKeyframeAvatar(Character character)
	{
		//RemoveLaterKeyFramesFor (character.id);
		KeyframeBase keyframe = new KeyframeBase ();
		keyframe.time = uiTimeline.timer;

		KeyframeAvatar keyframeAvatar = new KeyframeAvatar ();
		keyframeAvatar.avatarID = character.data.id;
		keyframeAvatar.action = character.actions.action.ToString();
		keyframeAvatar.expression = character.customizer.value;
		keyframe.avatar = keyframeAvatar;

		keyframe.pos = character.transform.position;
		return keyframe;
	}

	void OnPlaying(bool isPlaying) 
	{
		print("OnPlaying   " + isPlaying);
		if (isPlaying) {
			fullDuration = GetDuration ();	

			timer = uiTimeline.timer;	
			print("fullDuration   " + fullDuration +  " timer: " + timer);
			foreach (KeyframeBase keyFrame in activeScenesTimeline.keyframes)
				keyFrame.played = false;
		}
	}
	void Update()
	{
        if (uiTimeline.state == UITimeline.states.PLAY_ALL ||
            uiTimeline.state == UITimeline.states.PLAYING ||
            uiTimeline.state == UITimeline.states.RECORDING) {
            timer += Time.deltaTime;

            print("activeSceneID: " + activeSceneID + "  Update id: " + activeScenesTimeline.id + "   keyframes count: " + activeScenesTimeline.keyframes.Count);
            foreach (KeyframeBase keyFrame in activeScenesTimeline.keyframes) {
				if (keyFrame.time <= timer && keyFrame.played == false) {
					SetActiveKeyFrame (keyFrame);
					keyFrame.played = true;
				}
			}
			if (uiTimeline.state == UITimeline.states.PLAYING && timer >= fullDuration) {
				Events.OnPlaying (false);
			}
            if (uiTimeline.state == UITimeline.states.PLAY_ALL && timer >= fullDuration)
            {
                
                activeSceneID++;
                print("cambia a:" + activeSceneID + " scene id: " + World.Instance.scenesManager.sceneActive.id);
                if (activeSceneID==scenesTimeline.Count)
                {
                    World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(false);
                    Events.OnPlaying(false);
                    return;
                }                
                
                OnActivateScene(activeSceneID);
                fullDuration = GetDuration();
              //  World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(true);
                print("nuevo  scene id: " + World.Instance.scenesManager.sceneActive.id);
                timer = 0;
            }
        }
	}
    void OnActivateScene(int id)
    {
        timer = 0;
        activeSceneID = id;
        
        scenesManager.OnActivateScene(id);
        activeScenesTimeline = GetActiveScenesTimeline();
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
	public List<KeyframeBase> GetkeyframesOfAvatar(int avatarID)
	{
		List<KeyframeBase> keyframes = new List<KeyframeBase>();
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.avatar.avatarID == avatarID)
				keyframes.Add (keyFrame);
		}
		return keyframes;
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
			charactersManager.PositionateCharacter (character.data.id, GetLastPositionInTime (character.data.id, _duration));
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
			charactersManager.PositionateCharacter (character.data.id, GetLastPositionInTime (character.data.id, _timer));
		}
        activeScenesTimeline = GetActiveScenesTimeline();
		timer = _timer;
    }
    public float GetDuration()
    {
        if (GetActiveScenesTimeline().screenTitle != null)
            return 5;

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
        foreach (ScenesTimeline st in scenesTimeline)
            foreach (KeyframeBase keyFrame in st.keyframes)
                keyFrame.played = false;

        activeScenesTimeline = GetActiveScenesTimeline();
        timer = 0;
		activeSceneID = 0;
	}
	public void DeleteKeyframe(int characterID, float _timer)
	{
		KeyframeBase theKeyframe = null;
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.avatar.avatarID == characterID && keyFrame.time==_timer)
				theKeyframe = keyFrame;
		}
		if (theKeyframe != null)
			GetActiveScenesTimeline ().keyframes.Remove (theKeyframe);
	}
}
