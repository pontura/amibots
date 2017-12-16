using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenTitle : MonoBehaviour {

	public GameObject panel;
	public InputField field;
	public Text title;
	public Text cancelField;
    public bool intro;
    int editingID = -1;

	void Start () {
		intro = true;
		panel.SetActive (false);
		cancelField.text = "No, thanks!";
	}
    public void Edit(int sceneID)
    {
        editingID = sceneID;
        panel.SetActive(true);
        title.text = "Need a new title?";
        field.text = World.Instance.timeLine.scenesTimeline[sceneID].screenTitle.title;
    }
    public void Open()
	{
        if (World.Instance.timeLine.scenesTimeline.Count == 0)
            title.text = "Do you want to start with a title?";
        else 
            title.text = "Write a new title";
        field.text = "";
        panel.SetActive (true);
	}
	public void Ok () 
	{
		if (field.text.Length < 1)
			return;
        if (editingID != -1)
        {
            World.Instance.scenesManager.OnUpdateTitleScreen(editingID, field.text);
        }
        else
        {
            Events.AddKeyFrameScreenTitle(field.text, 0);
            GetComponent<UIAllScenesMenu>().AddNewTitleScene(0, field.text);
            if (intro == true)
                GetComponent<UISceneSelector>().Open(true);
            else
                Invoke("Delayed", 0.5f);
        }

		Close ();
	}
    void Delayed()
    {
        World.Instance.scenesManager.ActivateLastNonTitleScene();
    }
	public void Cancel () 
	{
		if (intro == true)
			GetComponent<UISceneSelector> ().Open (true);

		Close ();
	}
	void Close()
	{
        print(intro);
        editingID = -1;
        intro = false;
        panel.SetActive (false);
		cancelField.text = "Cancel";
	}
}
