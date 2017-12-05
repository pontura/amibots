using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

using System.Collections.Generic;


public class InputManager : MonoBehaviour
{
    public states state;
	ScenesManager scenesManager;
    Camera c;

    public enum states
    {
        IDLE,
        PRESSING,
        DRAGGING
    }

    bool dragStart;
	void Start()
	{
		scenesManager = GetComponent<ScenesManager> ();
        Events.OnChangeBackground += OnChangeBackground;

    }
    void OnChangeBackground(int id)
    {
        c = World.Instance.scenesManager.cam;
    }

    bool CanCompute()
	{
		if (EventSystem.current.currentSelectedGameObject != null)
			return false;
		if (EventSystem.current.IsPointerOverGameObject())
			return false;
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ())
			return false;

		return true;
	}
    void Update()
    {
       
		

#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = scenesManager.cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                ReleaseOnSceneObject(hit);
            Events.OnEndDrag();
        }
        if (CanCompute() && Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray ray = scenesManager.cam.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                HitOnSceneObject(hit);
        }

#else
        if (Input.touchCount > 0)
        {
			Touch touch = Input.touches [0];
			if (CanCompute() && touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = c.ScreenPointToRay(touch.position);
				if (Physics.Raycast (ray, out hit))
				    HitOnSceneObject(hit);
        
		    } else if (touch.phase == TouchPhase.Ended)
            {
                RaycastHit hit;
                Ray ray = c.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit))
                    ReleaseOnSceneObject(hit);
                Events.OnEndDrag ();
            }
        }
#endif

    }
    void HitOnSceneObject(RaycastHit hit)
    {
        if (hit.collider != null && hit.collider.gameObject.tag == "SceneObject")
            Events.ClickedOnSceneObject(hit.collider.gameObject.GetComponentInParent<SceneObject>());
        else if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            Events.ClickedOnCharacter(hit.collider.gameObject.GetComponentInParent<Character>());
      
    }
    void ReleaseOnSceneObject(RaycastHit hit)
    {
        if (World.Instance.worldStates.state == WorldStates.states.CHARACTERS_EDITOR
            && !CanCompute())
            return;

        if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
            Events.ClickedOn(hit.collider.gameObject.GetComponent<Tile>());

        
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
