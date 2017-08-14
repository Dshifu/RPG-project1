using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Characters
{
    public class Energy : MonoBehaviour
    {

        [SerializeField] int enemyLayer = 9;
		[SerializeField] RawImage energyBar;
		[SerializeField] float maxEnergyPoints = 100f;
        [SerializeField] float pointPerHit = 10f; 
		float currentEnergyPoints;

		CameraRaycaster cameraRaycaster;
        void Start()
        {
			currentEnergyPoints = maxEnergyPoints;
            RegisterForMouseRightClick();
        }

        void RegisterForMouseRightClick()
        {
            //cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            cameraRaycaster.notifyMouseRightClickObservers += OnMouseRightClick;
        }

        void OnMouseRightClick(RaycastHit raycastHit, int layerHit)
        {
            float newEnergyPoints = currentEnergyPoints - pointPerHit;
            currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0, maxEnergyPoints);
            print("currentEnergy " + currentEnergyPoints);
            UpdateEnergyBar();
            // if (layerHit == enemyLayer)
            // {
            //     GameObject enemy = raycastHit.collider.gameObject;
            //     if (IsTargerInRange(enemy))
            //     {

            //         AttackTarget(enemy);
            //     }
            // }
        }

        // Update is called once per frame
        void UpdateEnergyBar()
        {
            float xValue = -(EnergyAsPersentage / 2f) - 0.5f;
            energyBar.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }

        float EnergyAsPersentage
        {
            get { return (currentEnergyPoints/maxEnergyPoints); }
        }

    }
}