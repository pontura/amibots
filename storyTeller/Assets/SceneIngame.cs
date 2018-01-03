using System.Collections;
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
    public string title;
    public GameObject canvas;
    
    public void InitTitle(int id, string _title)
    {
        this.id = id;
        title = _title;
        GetComponent<UIScreenTitleIngame>().Open(_title);
        Invoke("TakePicture", 0.2f);
    }

    public void Init(int id, int backgroundID)
    {
        GetComponent<UIScreenTitleIngame>().Close();
        this.id = id;
        tiles.Init(backgroundID);
    }

    public void ChangeBackground(int backgroundID)
	{
		this.backgroundID = backgroundID;
		string url = "scenes/" + backgroundID.ToString ();
		background.sprite = Resources.Load(url, typeof(Sprite)) as Sprite;
		Invoke("TakePicture", 0.2f);
	}
	public void TakePicture()
	{
		screenshot.TakeScreenshot (id);
	}
}
