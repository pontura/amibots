using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {

	public Button NewButton;
	public GameObject UiPlaying;
	public GameObject tootltipActionEmpty;
	public states state;
	public GameObject world;
	public enum states
	{
		PLAYING,
		EDITING,
		TRANSITING
	}

	void Start () {
		UiPlaying.SetActive (true);
		Events.OnUIFunctionChangeIconColor += OnUIFunctionChangeIconColor;
        Events.OnUIChangeState += OnUIChangeState;
    }
    void OnUIChangeState(states state)
    {
        if (state == states.PLAYING)
            UiPlaying.SetActive(true);
        else
            UiPlaying.SetActive(false);
    }

    Color _color;
	void OnUIFunctionChangeIconColor(Color color)
	{
		_color = color;
	}
	// Update is called once per frame
	void Update () {
		if (state != states.PLAYING)
			return;
		
		if (Input.GetMouseButtonDown(0)) {
            if (CharacterData.Instance.characterScripts.scripts.Count > 0)
                return;
            else
                Events.OnTooltip("Action Empty", Vector3.zero);
            
			tootltipActionEmpty.SetActive (true);
			CancelInvoke ();
			Invoke ("ResetTooltip", 2);
		}
	}
	void ResetTooltip()
	{
		tootltipActionEmpty.SetActive (false);
	}
    public void NewAction()
    {
        Events.OpenCategorySelector();
        UiPlaying.SetActive(false);
    }
	public void ButtonPressed()
	{		
		//UiEditing.SetActive (true);
		UiPlaying.SetActive (false);
		state = states.EDITING;
		world.SetActive (false);
	}
	public void BakToGame()
	{
		state = states.TRANSITING;
		Invoke ("BackToGameDone", 0.1f);
	}
	public void BackToGameDone()
	{
		state = states.PLAYING;
		
		UiPlaying.SetActive (true);
		world.SetActive (true);
	}
}
