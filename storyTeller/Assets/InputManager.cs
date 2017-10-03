using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

using System.Collections.Generic;


public class InputManager : MonoBehaviour
{
    public states state;
	public Camera c;

    public enum states
    {
        IDLE,
        PRESSING,
        DRAGGING
    }
    bool dragStart;
    void Update()
    {
       
        if (EventSystem.current.currentSelectedGameObject != null)
            return;
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ())
			return;
//        if (IsPointerOverUIObject())
  //          return;




  ////////////////// esta funciona en android:
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                //prevent touch through
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;

                RaycastHit hit;
                Ray ray = c.ScreenPointToRay(touch.position);
				if (Physics.Raycast (ray, out hit))
				if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
				{
					Events.ClickedOn (hit.collider.gameObject.GetComponent<Tile>());
				}
            }
        }
////////////////////////////////////
        if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit;
			Ray ray = c.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit))
			if (hit.collider != null && hit.collider.gameObject.tag == "Tile")
			{
				Events.ClickedOn (hit.collider.gameObject.GetComponent<Tile>());
			}
		}
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
