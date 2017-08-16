using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class PlayerMovement : MonoBehaviour
    {
        CameraRaycaster cameraRaycaster = null;
        AICharacterControl aiCharacterControl = null;
        GameObject walkTarget = null;

        void Start()
        {
            RegisterDelegates();

            aiCharacterControl = GetComponent<AICharacterControl>();

            walkTarget = new GameObject("walkTarget");
        }

        void RegisterDelegates()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            cameraRaycaster.onMouseOverPotentionalTerrain += OnMouseOverPotentiallyWalkable;
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        void OnMouseOverPotentiallyWalkable(Vector3 destination)
        {
            if(Input.GetMouseButton(0))
            {
                walkTarget.transform.position = destination;
                aiCharacterControl.SetTarget(walkTarget.transform);
            }
        }

        void OnMouseOverEnemy(Enemy enemy)
        {
            if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                aiCharacterControl.SetTarget(enemy.transform);
            }
        }
    }
}