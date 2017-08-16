using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI;
using RPG.Core;

namespace RPG.Characters
{
    public class Player : MonoBehaviour, IDamageable
    {
        [SerializeField] float maxHealthPoints = 100f;
        float currentHealthPoints;

        [SerializeField] float damagePerHit = 10f;

        [SerializeField] Weapons weaponInUse;

        [SerializeField] AnimatorOverrideController animatorOverrideController;
        Animator animator;

        float lastHitTime = 0f;

        CameraRaycaster cameraRaycaster;
        
        void IDamageable.TakeDamage(float damage)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        }
        public float healthAsPercentage
        {
            get { return (currentHealthPoints / maxHealthPoints); }
        }

        void Start()
        {
            RegisterForMouseClick();
            SetCurrentMaxHealth();
            SetupRuntimeAnimator();
            PutWeaponInHand();
        }

        void RegisterForMouseClick()
        {
            cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        void OnMouseOverEnemy(Enemy enemy)
        {
            if(Input.GetMouseButton(0) && IsTargerInRange(enemy.gameObject))
            {
                AttackTarget(enemy);
            }
        }

        void SetCurrentMaxHealth()
        {
            currentHealthPoints = maxHealthPoints;
        }

        void SetupRuntimeAnimator()
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController["DEFAULT ATTACK"] = weaponInUse.GetAttackAnimClip(); 
        }

        void PutWeaponInHand()
        {
            GameObject weaponPrefab = weaponInUse.GetWeaponPrefab();
            GameObject dominantHand = RequestDominantHand();
            GameObject weapon = Instantiate(weaponPrefab, dominantHand.transform);
            weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
            weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
        }

        GameObject RequestDominantHand()
        {
            var dominantHands = GetComponentsInChildren<DominantHand>();
            int numberOfDominantHands = dominantHands.Length;
            Assert.AreNotEqual(numberOfDominantHands, 0, "No dominantHand fount on player");
            Assert.IsFalse(numberOfDominantHands > 1, "More than 1 DdominantHand scripts on player");
            return dominantHands[0].gameObject;
        }
        
        bool IsTargerInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponInUse.GetMaxAttackRange();
        }

        void AttackTarget(Enemy enemy)
        {
                if (Time.time - lastHitTime > weaponInUse.GetMinTimeBetweenHits())
                {
                    animator.SetTrigger("Attack");
                    enemy.TakeDamage(damagePerHit);
                    lastHitTime = Time.time;
                }
        }

        void OnDrawGizmos()
        {
            //Draw Attack Radius
            Gizmos.color = new Color(255f, 0f, 0f, 255f);
            Gizmos.DrawWireSphere(transform.position, weaponInUse.GetMaxAttackRange());

        }

    }
}