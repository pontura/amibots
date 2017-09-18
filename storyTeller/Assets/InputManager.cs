using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public states state;

    public enum states
    {
        IDLE,
        PRESSING,
        DRAGGING
    }
    bool dragStart;
    void Update()
    {
    }
    
}
