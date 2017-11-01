﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIngame : MonoBehaviour {

	public int id;
	public int backgroundID;
	public List<Character> characters;
	public Tiles tiles;
	public Transform tilesContainer;
	public Transform sceneObjects;
	public SpriteRenderer background;
	public HiResScreenshots screenshot;

	public Camera cam;

	public void ChangeBackground(int backgroundID)
	{
		this.backgroundID = backgroundID;
		string url = "scenes/" + backgroundID.ToString ();
		background.sprite = Resources.Load(url, typeof(Sprite)) as Sprite;
		Invoke("TakePicture", 0.2f);
	}
	void TakePicture()
	{
		print ("TakePicture" + id);
		screenshot.TakeScreenshot (id);
	}
}
