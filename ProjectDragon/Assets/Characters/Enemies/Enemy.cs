using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float chaseRadius = 8f;
	[SerializeField] float attackRadius = 4f;
	[SerializeField] float damagePerShot = 5f;
	[SerializeField] float secondsBetweenShots = 0.5f;
	bool isAttacking = false;
	AICharacterControl aiCharacterControl = null;
	[SerializeField] GameObject projectileToUse;
	[SerializeField] GameObject projectileSocket;
	[SerializeField] Vector3 aimOffset = new Vector3(0,2f,0);
	GameObject player = null;
	

	float currentHealthPoints;

	public float healthAsPercentage
	{
		get
		{
			return (currentHealthPoints / maxHealthPoints);
		}
	}
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aiCharacterControl = GetComponent<AICharacterControl>();
		currentHealthPoints = maxHealthPoints;
	}

	void Update()
	{
		float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		if(distanceToPlayer <= attackRadius && !isAttacking)
		{
			isAttacking = true;
			InvokeRepeating("SpawnProjectile", 0f, secondsBetweenShots);
		}
		if(distanceToPlayer > attackRadius)
		{
			isAttacking = false;
			CancelInvoke();
		}

		if(distanceToPlayer <= chaseRadius)
		{
			aiCharacterControl.SetTarget(player.transform);
		}
		else
		{
			aiCharacterControl.SetTarget(transform);
		}
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
		if (currentHealthPoints <= 0) { Destroy(gameObject); }
	}


	void DoDamage(float damageCause)
	{
		Component damageableComponent = player.gameObject.GetComponent(typeof(IDamageable));
		if (damageableComponent)
		{
			(damageableComponent as IDamageable).TakeDamage(damageCause);
		}
	}

	void SpawnProjectile()
	{
		GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position, Quaternion.identity);
		Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
		projectileComponent.ChangeDamage(damagePerShot);
		Vector3 unitVectorToPlayer = (player.transform.position + aimOffset - projectileSocket.transform.position).normalized;
		float projectileSpeed = projectileComponent.projectileSpeed;
		newProjectile.GetComponent<Rigidbody>().velocity =  unitVectorToPlayer * projectileSpeed;
	}

	void OnDrawGizmos()
	{
		//Draw chase Radius
		Gizmos.color = new Color (0f, 0f, 1, 1);
		Gizmos.DrawWireSphere(transform.position, chaseRadius);
		
		//Draw Attack Radius
		Gizmos.color = new Color (255f, 0f, 0f, 255f);
		Gizmos.DrawWireSphere(transform.position, attackRadius);

	}
}
