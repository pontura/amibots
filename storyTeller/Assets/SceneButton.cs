using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour {
    
	public Image image;
	UISceneSelector sceneSelector;
	UIAllScenesMenu allSceneMenu;
	public int id;
    public int backgroundID;
	HiResScreenshots hiResScreenshots;
    UiPopupScenes popUpScenes;
    public Color colorOn;

	void Start()
	{
        if (World.Instance.scenesManager.sceneActive != null)
			hiResScreenshots = World.Instance.scenesManager.sceneActive.GetComponent<HiResScreenshots> ();
	}
	public void InitInMenu(UIAllScenesMenu allSceneMenu, int _id)
	{		
		this.id = _id;
		this.allSceneMenu = allSceneMenu;
        popUpScenes = allSceneMenu.GetComponent<UiPopupScenes>();
        Events.UpdateThumbButton += UpdateThumbButton;
	}
	public void UpdateThumbButton(int sceneID)
	{
		if (id == sceneID)
			Invoke ("UpdateThumbButton2", 0.2f);
	}
	public void UpdateThumbButton2()
	{
        print("scene id en el thumbbutton es: " + id);
        Texture2D texture = hiResScreenshots.screenShot;
        image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        hiResScreenshots.ResetImage ();
	}
	public void Init(UISceneSelector sceneSelector,  int _id, int backgroundID)
	{
		this.id = _id;
		this.backgroundID = backgroundID;
		this.sceneSelector = sceneSelector;
		image.sprite = Resources.Load("scenes/" + backgroundID, typeof(Sprite)) as Sprite;
        image.maskable = true;
	}
    
	public void Clicked()
	{
        if (allSceneMenu != null)
        {
            popUpScenes.Init(id);
            GetComponent<Image>().color = colorOn;
        }
        else
        {
            sceneSelector.SetSelected(id, backgroundID);
        }
	}
    public void ResetSelected()
    {
        GetComponent<Image>().color = Color.white;
    }
}
