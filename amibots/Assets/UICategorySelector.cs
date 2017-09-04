using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICategorySelector : MonoBehaviour {

    public GameObject panel;

	void Start () {
        Close();
        Events.OpenCategorySelector += OpenCategorySelector;
    }
    void OpenCategorySelector()
    {
        panel.SetActive(true);
    }
    public void Clicked(AmiScript.categories category)
    {
        Events.EditNameOfAction(category);
        panel.SetActive(false);
    }
    public void Close()
    {
        panel.SetActive(false);
        Events.OnUIChangeState(UIGame.states.PLAYING);
    }
}
