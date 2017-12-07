using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour {

	public SceneIngame sceneIngame;
	public Transform container;
	public List<SceneIngame> scenesIngame;
	public SceneIngame sceneActive;
    public Camera cam;
    TimeLine timeline;
    TimeLine.ScenesTimeline activeScenesTimeline;

    void Start()
	{
        timeline = GetComponent<TimeLine>();

        Events.AddNewTitleScene += AddNewTitleScene;
        Events.AddNewScene += AddNewScene;
		Events.OnActivateScene += OnActivateScene;
	}
	public void OnActivateScene(int id)
	{        
		sceneActive = scenesIngame [id];
		Activate ();
        activeScenesTimeline = timeline.scenesTimeline[id];
        Events.OnChangeBackground(sceneActive.backgroundID);
        Events.NewSceneActive(id);
	}
    void AddNewTitleScene(int sceneID, string title)
    {
        sceneActive = Instantiate(sceneIngame);
        sceneActive.transform.SetParent(container);
        sceneActive.InitTitle(sceneID, title);
        scenesIngame.Add(sceneActive);
        Activate();
    }

    public void AddNewScene(int sceneID, int backgroundID)
	{
		sceneActive = Instantiate (sceneIngame);
		sceneActive.transform.SetParent (container);
        sceneActive.Init(sceneID, backgroundID);

        scenesIngame.Add (sceneActive);
		Activate();
		Events.OnChangeBackground (backgroundID);
        Events.RefreshCharacters();
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
