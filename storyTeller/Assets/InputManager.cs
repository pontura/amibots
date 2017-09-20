using UnityEngine;
using System.Collections;

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
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject ())
			return;
		if (Input.GetMouseButtonUp(0)) {
			RaycastHit hit;
			Ray ray = c.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit))
			if (hit.collider != null)
				Events.ClickedOn(hit.point);
		}
    }
    
}
