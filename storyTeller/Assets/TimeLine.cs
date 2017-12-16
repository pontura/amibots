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
        Events.AddNewTitleScene += AddNewTitleScene;
        Events.OnActivateScene += OnActivateScene;
        Events.OnDeleteScene += OnDeleteScene;

    }
	public ScenesTimeline GetActiveScenesTimeline()
	{
       // return scenesTimeline [scenesManager.sceneActive.id];
        return scenesTimeline[activeSceneID];
    }
    void RefreshKeyframe()
    {

    }
    void AddNewTitleScene(int id, string title )
    {
        activeSceneID = scenesTimeline.Count;
    }
    void AddNewScene(int id, int backgroundID)
	{
        activeSceneID = scenesTimeline.Count;
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
                AddKeyframe(keyframe);
            }
        }      
		if (uiTimeline.state != UITimeline.states.RECORDING)
			return;
        if (keyframe == null)
			return;
        AddKeyframe(keyframe);
    }
	void AddKeyFrameNewCharacter(Character character)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe == null)
			return;
        keyframe.avatar.action = character.actions.action;
        keyframe.avatar.expression = "norm";
        AddKeyframe(keyframe);
    }
	void OnRecording(bool isRecording)
	{
        activeScenesTimeline = GetActiveScenesTimeline();
        int selectedAvatarID = charactersManager.selectedCharacter.data.id;

        if (isRecording) {
			RemoveLaterKeyFramesFor (selectedAvatarID);
            ResetPlayedKeyframes();
        }
            
            Character character = World.Instance.charactersManager.selectedCharacter;
            KeyframeBase keyframe = GetNewKeyframeAvatar(character);
            keyframe.avatar.action = character.actions.action;
            keyframe.avatar.expression = character.customizer.expression;

            if (keyframe == null)
                return;
            AddKeyframe(keyframe);
        

        //foreach (Character character in World.Instance.scenesManager.sceneActive.characters)
        //{
        //    KeyframeBase keyframe = GetNewKeyframeAvatar(character);
        //    if (keyframe == null)
        //        return;
        //    AddKeyframe(keyframe);
        //}
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
        keyframe.avatar.expression = character.customizer.expression;
        keyframe.avatar.action = character.actions.action;
        AddKeyframe(keyframe);
    }
	void AddKeyFrameAction(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.action = value;
        keyframe.avatar.expression = character.customizer.expression;
        AddKeyframe(keyframe);
    }
	void AddKeyFrameExpression(Character character, string value)
	{
		KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.expression = value;
        keyframe.avatar.action = character.actions.action;
        AddKeyframe(keyframe);
    }
	void OnCharacterSay(int characterID, string value)
	{
        Character character = World.Instance.charactersManager.GetCharacter(characterID);
        KeyframeBase keyframe = GetNewKeyframeAvatar (character);
		if (keyframe.avatar == null) return;
		keyframe.avatar.chat = value;
        keyframe.avatar.action = character.actions.action;
        keyframe.avatar.expression = character.customizer.expression;
        AddKeyframe(keyframe);
    }
    void AddKeyframe(KeyframeBase _keyframe)
    {
        int idToChange = -1;
        int id = 0;
        foreach (KeyframeBase keyFrame in activeScenesTimeline.keyframes)
        {
            if (keyFrame.avatar.avatarID == _keyframe.avatar.avatarID && keyFrame.time == _keyframe.time)
            {
                idToChange = id;
            }
            id++;
        }
       ScenesTimeline st =  activeScenesTimeline;
        if (idToChange != -1 && st.keyframes.Count>= idToChange)
            st.keyframes[idToChange] = _keyframe;
        else
            GetActiveScenesTimeline().keyframes.Add(_keyframe);
    }
    float GetRoundedTimer(float _timer)
    {
        float t = Mathf.Round(_timer * 10) / 10;
        return t;
    }
	KeyframeBase GetNewKeyframeAvatar(Character character)
	{
		//RemoveLaterKeyFramesFor (character.id);
		KeyframeBase keyframe = new KeyframeBase ();
		keyframe.time = GetRoundedTimer(uiTimeline.timer);

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
		if (isPlaying) {			
            ResetPlayedKeyframes();
        }
	}
    void ResetPlayedKeyframes()
    {
        fullDuration = GetDuration();
        timer = GetRoundedTimer( uiTimeline.timer );
        foreach (KeyframeBase keyFrame in activeScenesTimeline.keyframes)
        {
            if(keyFrame.time > timer)
                keyFrame.played = false;
            else
                keyFrame.played = true;
        }
    }
	void Update()
	{
        if (uiTimeline.state == UITimeline.states.PLAY_ALL ||
            uiTimeline.state == UITimeline.states.PLAYING ||
            uiTimeline.state == UITimeline.states.RECORDING) {
            timer += Time.deltaTime;

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
               // print("cambia a:" + activeSceneID + " scene id: " + World.Instance.scenesManager.sceneActive.id + " count: " + scenesTimeline.Count);
                if (activeSceneID == scenesTimeline.Count-1)
                {
                    World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(false);
                    Events.OnPlaying(false);
                    return;
                }
                activeSceneID++;
                uiTimeline.timer = 0;

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
    void OnDeleteScene(int sceneID)
    {
        print("OnDeleteScene " + sceneID);
        ScenesTimeline sceneSelected = null;
        foreach (ScenesTimeline stl in scenesTimeline)
        {
            if(stl.id == sceneID)
                sceneSelected = stl;
        }
        scenesTimeline.Remove(sceneSelected);
        OnActivateScene(0);        
        World.Instance.scenesManager.OnDeleteScene(sceneID);
        Events.OnActivateScene(0);
    }

    void SetActiveKeyFrame(KeyframeBase keyFrame)
    {
        print("__SetActiveKeyFrame " + keyFrame);
		if (keyFrame.avatar != null) {
			KeyframeAvatar keyframeAvatar = keyFrame.avatar;
			int avatarID = keyFrame.avatar.avatarID;
			string action = keyFrame.avatar.action;
			string expression = keyFrame.avatar.expression;
			string chat = keyFrame.avatar.chat;
			Vector3 moveTo = keyFrame.moveTo;
			Vector3 pos = keyFrame.pos;

			if (chat != null && chat.Length >0) {
				uiTimeline.GetComponent<UIChatManager>().OnCharacterSay(avatarID, chat);
			} else
            if (moveTo != Vector3.zero)
            {               
                charactersManager.MoveCharacter(avatarID, moveTo);
            }
            else
            {
                charactersManager.PositionateCharacter(avatarID, pos);
                charactersManager.ChangeExpression(avatarID, expression);
                charactersManager.CharacterAction(avatarID, action);
            }
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
        ScenesTimeline activeScene = GetActiveScenesTimeline();
        print("rewind all: " + activeScene.id);
        foreach (KeyframeBase keyFrame in activeScene.keyframes) {
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
        if (GetActiveScenesTimeline().screenTitle != null 
            &&
            GetActiveScenesTimeline().screenTitle.title != null)
            return 3;

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
		List<KeyframeBase> theKeyframes = new List<KeyframeBase>();
		foreach (KeyframeBase keyFrame in GetActiveScenesTimeline().keyframes) {
			if (keyFrame.avatar.avatarID == characterID && keyFrame.time>=_timer)
                theKeyframes.Add( keyFrame);
		}
        foreach (KeyframeBase keyFrame in theKeyframes)
        {
            GetActiveScenesTimeline().keyframes.Remove(keyFrame);
        }
	}
}
