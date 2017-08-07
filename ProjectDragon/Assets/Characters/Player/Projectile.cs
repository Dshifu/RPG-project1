using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	public float projectileSpeed;
	float damageCause ;

	public void ChangeDamage(float damage)
	{
		damageCause = damage;
	}
	void OnCollisionEnter(Collision collision)
	{
		//print ("Challenge complete." + collider.gameObject);
		Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
		if (damageableComponent)
		{
			(damageableComponent as IDamageable).TakeDamage(damageCause);
		}
		Destroy(gameObject, 0.025f);
	}

}
