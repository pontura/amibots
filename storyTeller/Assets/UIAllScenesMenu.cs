using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAllScenesMenu : MonoBehaviour {

	public SceneButton sceneButton;
	public Transform container;

	SceneButton activeSceneButton;
	public int activeSceneID;
    public List<SceneButton> all;

    void Start()
    {
        Events.OnDeleteScene += OnDeleteScene;
        Events.OnDuplicate += OnDuplicate;
    }
    public void SetActive(int id)
	{
		Events.OnActivateScene (id);

    }
    void OnDuplicate(int id)
    {
        SceneButton sb = null;
        foreach (SceneButton b in all)
        {
            if (b.id == id)
                sb = b;
        }
    }
    void OnDeleteScene(int id)
    {
        SceneButton sb = null;
        foreach (SceneButton b in all)
        {
            if(b.id == id)
            sb = b;
        }
        all.Remove(sb);
        Destroy(sb.gameObject);
    }
    public void AddNewTitleScene(int sceneID, string title)
    {
       // GetComponent<UISceneSelector>().Open(true);
       // activeSceneID++;
        SceneButton newSceneButton = Instantiate(sceneButton);
        newSceneButton.transform.SetParent(container);
        newSceneButton.transform.localScale = Vector2.one;
        print("AddNewTitleScene " + sceneID);
        newSceneButton.InitInMenu(this, activeSceneID);
        activeSceneButton = newSceneButton;
        Events.AddNewTitleScene(activeSceneID, title);
        all.Add(newSceneButton);
        activeSceneID++;
    }
    public void AddNewScene(int sceneID, int backgroundID)
	{
        GetComponent<UISceneSelector> ().Open (true);
		
		SceneButton newSceneButton = Instantiate (sceneButton);
		newSceneButton.transform.SetParent (container);
		newSceneButton.transform.localScale = Vector2.one;
		print ("AddNewScene id: " + sceneID);

        newSceneButton.InitInMenu (this, activeSceneID);
		activeSceneButton = newSceneButton;
        all.Add(newSceneButton);
        Events.AddNewScene (activeSceneID, backgroundID);
        activeSceneID++;
    }
    public void ResetAllButtonsSelected()
    {
        foreach (SceneButton sceneButton in container.GetComponentsInChildren< SceneButton>())
        {
            sceneButton.ResetSelected();
        }
    }
}
