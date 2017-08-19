using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeLine : MonoBehaviour {

	public GameObject functionsLineContainer;

	public List<UIFunctionLine> allFunctions;

	bool isPlaying;
	public float timer;

	public Character character;
	bool characterFalled;
	//Secuencia activa son los bloques de funciones divididos por "Waits":
	public int activeSequence;

	void Start () {
		Events.OnDebug += OnDebug;	
		Events.CharacterFall += CharacterFall;
	}
	void Update()
	{
		if (!isPlaying)
			return;
		if (allFunctions.Count == 0)
			return;
		ComputeAllActives ();
	}
	void OnDebug(bool _isPlaying)
	{
		
		characterFalled = false;
		this.isPlaying = _isPlaying;

		activeSequence = 0;
		timer = 0;

		allFunctions.Clear ();
		if (isPlaying)
			CatchFunctions ();
	}
	void CatchFunctions()
	{
		Events.OnUIFunctionChangeIconColor (Color.grey);
		int sequenceID = 0;
		foreach (UIFunctionLine uifl in functionsLineContainer.GetComponentsInChildren<UIFunctionLine>()) {
			uifl.sequenceID = sequenceID;
			uifl.done = false;
			allFunctions.Add (uifl);
			if (IfWaitFunctionStopsRoutine (uifl))
				sequenceID++;
		}
	}
	void CharacterFall(Character.states state)
	{
		characterFalled = true;
	}
	void ComputeAllActives()
	{
		timer += Time.deltaTime;
		float keyFrame = 0;
		bool someFunctionIsActive = false;
		foreach (UIFunctionLine uifl in allFunctions) {
			if (uifl.sequenceID <= activeSequence && !uifl.done) {
				someFunctionIsActive = true;
				if (IfWaitFunctionStopsRoutine (uifl))
					return;
				UpdateFuncion (uifl);
				if(!characterFalled)
					character.UpdateFunctions (uifl.function.variables, timer);
			}
		}
		if (!someFunctionIsActive) {
			character.Reset ();
			allFunctions.Clear ();
			print (character.body.transform.localPosition.z);
            if(characterFalled)
                Events.OnUIFunctionChangeIconColor(Color.red);
            else if (character.body.transform.localPosition.z > 1)
				Events.OnUIFunctionChangeIconColor (Color.green);
			else
				Events.OnUIFunctionChangeIconColor (Color.yellow);
			Events.OnDebug (false);
		}
	}
	void UpdateFuncion(UIFunctionLine uifl)
	{
		float duration = (float)GetFunctionDuration (uifl.function);
		if (duration >= timer) {
			uifl.SetFilled (timer / duration);
		} else {
			uifl.IsReady ();
		}
	}
	float GetFunctionDuration(AmiFunction function)
	{
		foreach (AmiClass amiClass in function.variables) {
			if (amiClass.type == AmiClass.types.TIME)
				return float.Parse(amiClass.className);
		}
		return 0;
	}
	bool IfWaitFunctionStopsRoutine(UIFunctionLine uifl)
	{
		foreach (AmiClass amiClass in uifl.function.variables) {
			if (amiClass.type == AmiClass.types.WAIT) {
				if (timer < float.Parse (amiClass.className)) {
					return true;
				} else {
					timer = 0;
					activeSequence++;
					uifl.IsReady ();
				}
			}
		}
		return false;
	}
}