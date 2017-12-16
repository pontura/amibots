using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPopupScenes : MonoBehaviour {

    public GameObject panel;
    int id;
    UIAllScenesMenu allSceneMenu;
    public Button deleteButton;

    void Start () {
        allSceneMenu = GetComponent<UIAllScenesMenu>();
        panel.SetActive(false);
    }
	public void Init(int id) {
        this.id = id;
        panel.SetActive(true);
        if (World.Instance.timeLine.scenesTimeline.Count == 1)
            deleteButton.interactable = false;
        else
            deleteButton.interactable = true;
    }
    public void Edit()
    {
        print(id);
        if (World.Instance.timeLine.scenesTimeline[id].screenTitle== null 
            || World.Instance.timeLine.scenesTimeline[id].screenTitle.title == null)
            allSceneMenu.SetActive(id);
        else
            GetComponent<UIScreenTitle>().Edit(id);
        

        Close();
    }
    public void Duplicate()
    {
        Close();
        Events.OnDuplicate(id);
    }
    public void Delete()
    {
        Close();
        Events.OnDeleteScene(id);
    }
    public void Close()
    {
        GetComponent<UIAllScenesMenu>().ResetAllButtonsSelected();
        panel.SetActive(false);
    }
}
