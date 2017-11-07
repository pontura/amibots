﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPreview : MonoBehaviour {

    public Text field;
    public GameObject panel;
    TimeLine timeline;

	void Start () {
        timeline = World.Instance.timeLine;
        panel.SetActive(false);
    }

    public void Init()
    {
        World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(true);
        panel.SetActive(true);
    }
    public void Close()
    {
        World.Instance.scenesManager.cam.GetComponent<CameraInScene>().SetFilming(false);
        panel.SetActive(false);
    }
    public void Replay()
    {
        GetComponent<UITimeline>().PlayAllClicked();
    }
    void Update()
    {
        UpdateField();
    }
    void UpdateField()
    {
        int minutes = Mathf.FloorToInt(timeline.timer / 60) % 60;
        int seconds = Mathf.FloorToInt(timeline.timer - minutes * 60) % 60;
        int milliseconds = Mathf.FloorToInt((timeline.timer - (minutes) * 60) * 60) % 100;
        field.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}