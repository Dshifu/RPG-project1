using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CameraRaycaster))]

public class CursorAffordance : MonoBehaviour {

	[SerializeField] const int WalkableLayerNumber = 8;
	[SerializeField] const int EnemyLayerNumber = 9;

	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D atackCursor = null;
	[SerializeField] Texture2D errorCursor = null;
	[SerializeField] Vector2 cursorHotSpot = new Vector2 (0,0);

	CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
	cameraRaycaster = GetComponent<CameraRaycaster>();
	cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
	}
	
	// Update is called once per frame
	void OnLayerChanged (int newLayer) {
		//Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
		switch (newLayer)
            {
                    case WalkableLayerNumber:
                        Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                        break;
                    case EnemyLayerNumber:
                        Cursor.SetCursor(atackCursor, cursorHotSpot, CursorMode.Auto);
                        break;
                    default:
						Cursor.SetCursor(errorCursor, cursorHotSpot, CursorMode.Auto);
                    return;

            }  
			
	}
}
