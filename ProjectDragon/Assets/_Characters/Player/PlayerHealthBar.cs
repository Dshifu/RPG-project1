using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Characters
{
    [RequireComponent(typeof(RawImage))]
    public class PlayerHealthBar : MonoBehaviour
    {

        RawImage healthBarRawImage;
        Player player;

        void Start()
        {
            player = FindObjectOfType<Player>();
            healthBarRawImage = GetComponent<RawImage>();
        }

        void Update()
        {
           UpdateHealthPoints();
        }

        void UpdateHealthPoints()
        {
            float xValue = -(player.healthAsPercentage / 2f) - 0.5f;
            healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
    }
}