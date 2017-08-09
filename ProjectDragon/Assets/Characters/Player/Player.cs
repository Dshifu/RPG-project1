using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamageable {
	[SerializeField] int enemyLayer = 9;
	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float damagePerHit = 10f;
	[SerializeField] float minTimeBetweenHits = 0.5f;
	[SerializeField] float maxAttackRange = 2f;
	[SerializeField] Weapons weaponInUse;

	GameObject currentTarrget;
	float currentHealthPoints;

	CameraRaycaster cameraRaycaster;
	float lastHitTime =0f;

	public float healthAsPercentage
	{
		get { return (currentHealthPoints / maxHealthPoints); }
	}

	void Start()
	{
		cameraRaycaster = FindObjectOfType<CameraRaycaster>();
		cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
		currentHealthPoints = maxHealthPoints;

		PutWeaponInHand();
	}

	void PutWeaponInHand ()
	{
		var weaponPrefab = weaponInUse.GetWeaponPrefab();
		GameObject dominantHand = RequestDominantHand();
		var weapon = Instantiate(weaponPrefab, dominantHand.transform);
		weapon.transform.localPosition = weaponInUse.gripTransform.localPosition;
		weapon.transform.localRotation = weaponInUse.gripTransform.localRotation;
	}


	GameObject RequestDominantHand()
	{
		var dominantHands = GetComponentsInChildren<DominantHand>();
		int numberOfDominantHands = dominantHands.Length;
		Assert.AreNotEqual(numberOfDominantHands,0,"No dominantHand fount on player");
		Assert.IsFalse(numberOfDominantHands > 1, "More than 1 DdominantHand scripts on player");
		return dominantHands[0].gameObject;
	}
	void OnMouseClick (RaycastHit raycastHit, int layerHit)
	{
		if(layerHit == enemyLayer)
		{
			GameObject enemy = raycastHit.collider.gameObject;
			currentTarrget = enemy;

			if ((enemy.transform.position - transform.position).magnitude > maxAttackRange)
			{
				return;
			}
			//currentTarrget = enemy;
			Enemy enemyComponent = enemy.GetComponent<Enemy>();
			if (Time.time - lastHitTime > minTimeBetweenHits)
			{
				enemyComponent.TakeDamage(damagePerHit);
				lastHitTime = Time.time;
			}
		}
	}
	void IDamageable.TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
	}
	
	void OnDrawGizmos()
	{
		//Draw Attack Radius
		Gizmos.color = new Color (255f, 0f, 0f, 255f);
		Gizmos.DrawWireSphere(transform.position, maxAttackRange);

	}

}
