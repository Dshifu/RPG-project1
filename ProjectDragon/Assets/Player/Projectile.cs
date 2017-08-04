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
	void OnTriggerEnter(Collider collider)
	{
		//print ("Challenge complete." + collider.gameObject);
		Component damageableComponent = collider.gameObject.GetComponent(typeof(IDamageable));
		if (damageableComponent)
		{
			(damageableComponent as IDamageable).TakeDamage(damageCause);
		}
	}

}
