using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeLine : MonoBehaviour {

	public GameObject functionsLineContainer;

	public List<UIFunctionLine> allFunctions;
	public List<UIFunctionLine> activeFunctions;

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
		if (activeFunctions.Count == 0)
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
			activeFunctions.Add (uifl);
		}
	}
	void ComputeAllActives()
	{
		timer += Time.deltaTime;
		float keyFrame = 0;
		foreach (UIFunctionLine uifl in activeFunctions) {
			float duration = (float)GetFunctionDuration(uifl.function);
			if (duration >= timer) {
				uifl.SetFilled (timer / duration);
			} else {
				activeFunctions.Remove (uifl);
				break;
			}
			character.UpdateFunctions (uifl.function.variables, timer);
		}
		if (activeFunctions.Count == 0)
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
}