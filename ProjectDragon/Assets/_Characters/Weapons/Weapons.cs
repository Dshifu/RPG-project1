using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class Weapons : ScriptableObject
    {

        public Transform gripTransform;
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;
        [SerializeField] float minTimeBetweenHits = 0.5f;
        [SerializeField] float maxAttackRange = 2f;


        public float GetMinTimeBetweenHits()
        {
            return minTimeBetweenHits;
        }
        public float GetMaxAttackRange()
        {
            return maxAttackRange;
        }
        public GameObject GetWeaponPrefab()
        {
            return weaponPrefab;
        }

        public AnimationClip GetAttackAnimClip()
        {
            attackAnimation.events = new AnimationEvent[0]; //Remove animation events
            return attackAnimation;
        }
    }
}