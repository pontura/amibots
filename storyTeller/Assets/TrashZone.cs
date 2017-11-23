using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashZone : MonoBehaviour {

	public void TrashZoneEnter()
    {
        print("TRASH");
        Events.OnEndDrag();
    }
}
