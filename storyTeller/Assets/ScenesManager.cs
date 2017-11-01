using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour {

	public SceneIngame sceneIngame;
	public Transform container;
	public List<SceneIngame> scenesIngame;
	public SceneIngame sceneActive;
	void Start()
	{
		Events.AddNewScene += AddNewScene;
		Events.OnActivateScene += OnActivateScene;
	}
	void OnActivateScene(int id)
	{
		sceneActive = scenesIngame [id];
		Activate ();
		Events.OnChangeBackground (sceneActive.backgroundID);
		Events.NewSceneActive (id);
	}
	public void AddNewScene(int sceneID, int backgroundID)
	{
		print ("sceneID " + sceneID);
		sceneActive = Instantiate (sceneIngame);
		sceneActive.transform.SetParent (container);
		sceneActive.id = sceneID;

		scenesIngame.Add (sceneActive);
		Activate();
		Events.OnChangeBackground (backgroundID);
	}
	void Activate()
	{
		foreach (SceneIngame s in scenesIngame) {
			if (s.id != sceneActive.id) {
				s.gameObject.SetActive (false);
			} else {
				s.gameObject.SetActive (true);
			}
		}
	}
}
