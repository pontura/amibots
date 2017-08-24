using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public Vector3 startPos;

    float heroRotation;
    float heroRotationVertical;

    public states state;
    private float time_start_pressing;

    public Vector3 updateDraggingPosition;
    private Vector3 lastMousePos;
    private Vector2 dragPosition;

    public int forward;
	public UIGame UIGame;

    public enum states
    {
        IDLE,
        PRESSING,
        DRAGGING
    }
    bool dragStart;
    void Update()
    {
		if (UIGame.state != UIGame.states.PLAYING)
			return;
        //#if UNITY_ANDROID
        UpdateDevice();
        //#else
        if (Input.GetMouseButtonUp(0))
            Release();
        if (Input.GetMouseButtonDown(0))
            Press();
        //#endif
        if (state == states.PRESSING || state == states.DRAGGING)
        {
            if (!dragStart)
                StartDragging();
            else
                UpdateDragging();
        }
    }

    Vector3 p = new Vector3();
    public Camera c;
    Vector2 mousePos = new Vector2();

    void UpdateDevice()
    {

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = c.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null)
                    Events.ClickedOn(hit.point);

        }
        return;
        if (Input.touchCount > 0)
        {
            dragPosition = Vector2.zero;
            foreach (Touch t in Input.touches)
            {
                if (t.position.x > 250)
                    dragPosition = t.position;
            }
        }
    }
    void Press()
    {
        bool overUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (overUI) return;
        startPos = Input.mousePosition;
        state = states.PRESSING;
        time_start_pressing = Time.time;
    }
    void Release()
    {
        dragStart = false;
        lastMousePos = Vector3.zero;
        float distance = Vector3.Distance(startPos, Input.mousePosition);
        if (distance < 20) Events.ClickedOnScreen();
        state = states.IDLE;

       // Events.OnDragging(false);
    }
    void StartDragging()
    {
       // Events.OnDragging(true);
        dragStart = true;
    }
    void UpdateDragging()
    {
        if (lastMousePos != Vector3.zero)
            updateDraggingPosition = lastMousePos - Input.mousePosition;
        lastMousePos = Input.mousePosition;

        if (Vector3.Distance(startPos, lastMousePos) > 10)
            state = states.DRAGGING;
    }
}
