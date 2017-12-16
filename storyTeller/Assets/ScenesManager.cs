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
    public void OnDeleteScene(int sceneID)
    {
        SceneIngame sig = null;
        foreach (SceneIngame s in scenesIngame)
        {
            if (s.id == sceneID)
                sig = s;
        }
        scenesIngame.Remove(sig);
        Destroy(sig.gameObject);
        ActivateLastNonTitleScene();
    }

    int GetPositionByID(int sceneID)
    {
        int arrPos = 0;
        foreach (SceneIngame s in scenesIngame)
        {
            if (s.id == sceneID)
                return arrPos;
            arrPos++;
        }
        return 0;
    }
    public void OnUpdateTitleScreen(int id, string title)
    {
        World.Instance.timeLine.scenesTimeline[id].screenTitle.title = title;
        SceneIngame scene = scenesIngame[id];
        scene.InitTitle(id, title);
    }
    public void OnActivateScene(int id)
	{
        int arrPosID = GetPositionByID(id);
        timeline.activeSceneID = arrPosID;
        sceneActive = scenesIngame[arrPosID];
        //sceneActive = scenesIngame [id];
        Activate ();
        activeScenesTimeline = timeline.scenesTimeline[arrPosID];
        Events.OnChangeBackground(sceneActive.backgroundID);
        Events.NewSceneActive(arrPosID);
	}
    void AddNewTitleScene(int sceneID, string title)
    {
        print("AddNewTitleScene " + sceneID);
        sceneActive = Instantiate(sceneIngame);
        sceneActive.transform.SetParent(container);
        sceneActive.InitTitle(sceneID, title);
        scenesIngame.Add(sceneActive);
        Activate();
    }

    public void AddNewScene(int sceneID, int backgroundID)
	{
        print("AddNewScene " + sceneID);
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
    public void ActivateLastNonTitleScene()
    {
        int id = 0;
        foreach (SceneIngame s in scenesIngame)
        {
            print("___________" + s.title.Length);
            if (s.title.Length == 0)
                id = s.id;
        }
        Events.OnActivateScene(id);
    }
}
