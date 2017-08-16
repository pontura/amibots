using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeLine : MonoBehaviour {

	public GameObject functionsLineContainer;

	public List<UIFunctionLine> allFunctions;

	bool isPlaying;
	public float timer;

	public Character character;

	void Start () {
		Events.OnDebug += OnDebug;	
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
		this.isPlaying = _isPlaying;
		timer = 0;
		allFunctions.Clear ();
		if (isPlaying)
			CatchFunctions ();
	}
	void CatchFunctions()
	{
		foreach (UIFunctionLine uifl in functionsLineContainer.GetComponentsInChildren<UIFunctionLine>()) {
			allFunctions.Add (uifl);
		}
	}
	void ComputeAllActives()
	{
		timer += Time.deltaTime;
		float keyFrame = 0;
		foreach (UIFunctionLine uifl in allFunctions) {
			
			if (IfWaitFunctionStopsRoutine (uifl.function))
				return;
			
			float duration = (float)GetFunctionDuration(uifl.function);
			if (duration >= timer) {
				uifl.SetFilled (timer / duration);
			} else {
				allFunctions.Remove (uifl);
				break;
			}
			character.UpdateFunctions (uifl.function.variables, timer);

		}
		if (allFunctions.Count == 0)
			Events.OnDebug (false);
	}
	float GetFunctionDuration(AmiFunction function)
	{
		foreach (AmiClass amiClass in function.variables) {
			if (amiClass.type == AmiClass.types.TIME)
				return float.Parse(amiClass.className);
		}
		return 0;
	}
	bool IfWaitFunctionStopsRoutine(AmiFunction function)
	{
		foreach (AmiClass amiClass in function.variables) {
			if (amiClass.type == AmiClass.types.WAIT && timer<int.Parse(amiClass.className))
				return true;
		}
		return false;
	}
}