using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeLine : MonoBehaviour {

	public GameObject functionsLineContainer;

	public List<UIFunctionLine> allFunctions;

	bool isPlaying;
	public float timer;

	public Character character;
	
	//Secuencia activa son los bloques de funciones divididos por "Waits":
	public int activeSequence;

	void Start () {
		Events.OnDebug += OnDebug;	
		
	}
	void Update()
	{
		if (!isPlaying)
			return;
		if (allFunctions.Count == 0)
			return;
        character.scriptsProcessor.Compute(allFunctions);
	}
	void OnDebug(bool _isPlaying)
	{
		this.isPlaying = _isPlaying;

		activeSequence = 0;
		timer = 0;

		allFunctions.Clear ();
        if (isPlaying)
        {
            Events.OnUIFunctionChangeIconColor(Color.grey);
            CatchFunctions();
        }
	}
	void CatchFunctions()
	{		
		int sequenceID = 0;
		foreach (UIFunctionLine uifl in functionsLineContainer.GetComponentsInChildren<UIFunctionLine>()) {
			uifl.sequenceID = sequenceID;
			uifl.done = false;
			allFunctions.Add (uifl);
            sequenceID++;
		}
	}
    public void SaveFunction()
    {
        CatchFunctions();
        Events.SaveScript(allFunctions, "walk");
        allFunctions.Clear();
    }
	
}