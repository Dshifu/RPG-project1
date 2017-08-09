using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField] float projectileSpeed;

	const float Destroy_delay = 0.025f;
	GameObject shooter;
	float damageCause ;


	public void SetShooter (GameObject shooter)
	{
		this.shooter = shooter;
	}
	public void ChangeDamage(float damage)
	{
		damageCause = damage;
	}

	public float GetDefaultLaunchSpeed()
	{
		return projectileSpeed;
	}
	void OnCollisionEnter(Collision collision)
	{
		int layerCollidedWith = collision.gameObject.layer;
		if (layerCollidedWith != shooter.layer)
		{
			DamageDamagebles(collision);
		}

		//print ("Challenge complete." + collider.gameObject);

	}

	void DamageDamagebles(Collision collision)
	{		
		Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
		if (damageableComponent)
		{
			(damageableComponent as IDamageable).TakeDamage(damageCause);
		}
		Destroy(gameObject, Destroy_delay);
	}

}
