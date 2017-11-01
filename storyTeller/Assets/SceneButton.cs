using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour {

	public Texture thumbTexture;
	public Shader matShader;
	public Material mat;
	public RawImage rawImage;
	public Image image;
	UISceneSelector sceneSelector;
	UIAllScenesMenu allSceneMenu;
	public int id;
	int backgroundID;
	HiResScreenshots hiResScreenshots;
	void Start()
	{
		if(World.Instance.scenesManager.sceneActive != null)
			hiResScreenshots = World.Instance.scenesManager.sceneActive.GetComponent<HiResScreenshots> ();
	}
	public void InitInMenu(UIAllScenesMenu allSceneMenu, int _id)
	{		
		this.id = _id;
		this.allSceneMenu = allSceneMenu;
		Events.UpdateThumbButton += UpdateThumbButton;
	}
	public void UpdateThumbButton(int sceneID)
	{
		if (id == sceneID)
			Invoke ("UpdateThumbButton2", 0.2f);
	}
	public void UpdateThumbButton2()
	{
		mat = new Material (matShader);
		mat.mainTexture = hiResScreenshots.screenShot;
		image.material = mat;
		hiResScreenshots.ResetImage ();
	}
	public void Init(UISceneSelector sceneSelector,  int _id, int backgroundID)
	{
		this.id = _id;
		this.backgroundID = backgroundID;
		this.sceneSelector = sceneSelector;
		image.sprite = Resources.Load("scenes/" + backgroundID, typeof(Sprite)) as Sprite;
	}
	public void Clicked()
	{
		if (allSceneMenu != null)
			allSceneMenu.SetActive (id);
		else
			sceneSelector.SetSelected (id, backgroundID);
	}
}
