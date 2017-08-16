using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Characters;

namespace RPG.CameraUI
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D enemyCursor = null;

        [SerializeField] Vector2 cursorHotSpot = new Vector2(0, 0);

        const int POTENTIALLY_WALKABLE_LAYER = 8;

        float maxRaycastDepth = 100f; // Hard coded value

        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverPotentionalTerrain;


        public delegate void OnMouseOverEnemy(Enemy enemy);
        public event OnMouseOverEnemy onMouseOverEnemy;

        void Update()
        {
            // Check if pointer is over an interactable UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Stop looking for other objects
            }
            else
            {
                PerformRaycast();
            }
        }

        void PerformRaycast()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (RaycastForEnemy(ray)) { return; }
            if (RaycastForPottentionalWalkable(ray)) { return; }
        }

        bool RaycastForEnemy(Ray ray)
        {
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, maxRaycastDepth);
            GameObject gameObjectHit = hitInfo.collider.gameObject;
            Enemy enemyHit = gameObjectHit.GetComponent<Enemy>();
            if (enemyHit)
            {
                Cursor.SetCursor(enemyCursor, cursorHotSpot, CursorMode.Auto);
                onMouseOverEnemy(enemyHit);
                return true;
            }
            return false;
        }

        bool RaycastForPottentionalWalkable(Ray ray)
        {
            RaycastHit hitInfo;
            LayerMask potentiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER;
            bool potentiallyWalkableHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer);
            if (potentiallyWalkableHit)
            {
                Cursor.SetCursor(walkCursor, cursorHotSpot, CursorMode.Auto);
                onMouseOverPotentionalTerrain(hitInfo.point);
                return true;
            }
            return false;
        }
    }
}