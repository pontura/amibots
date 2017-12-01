using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRecScreen : MonoBehaviour {

    public GameObject panel;
    public GameObject topUI;
    public GameObject playAll;

    void Start () {
        panel.SetActive(false);
    }
	
	public void SetState(bool isOn) {
        panel.SetActive(isOn);
        topUI.SetActive(!isOn);
        playAll.SetActive(!isOn);
    }
}
