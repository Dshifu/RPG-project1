using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Characters
{
    public class Energy : MonoBehaviour
    {
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
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy +=OnMouseOverEnemy;
        }

        void OnMouseOverEnemy (Enemy enemy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UpdateEnergyPoints();
                UpdateEnergyBar();
            }
        }

        void UpdateEnergyPoints()
        {
            float newEnergyPoints = currentEnergyPoints - pointPerHit;
            currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0, maxEnergyPoints);
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