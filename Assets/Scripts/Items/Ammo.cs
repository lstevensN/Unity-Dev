using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour, Damage.IDamagable
{
	[SerializeField] protected AmmoData ammoData;

	public void OnDamage(GameObject target)
	{
		// apply damage if game object has health
		if (target.TryGetComponent<Damage.IDamagable>(out Damage.IDamagable damagable))
		{
			damagable.ApplyDamage(ammoData.damage * ((ammoData.damageOverTime) ? Time.deltaTime : 1));
		}

		// create impact prefab
		if (ammoData.impactPrefab != null)
		{
			Instantiate(ammoData.impactPrefab, transform.position, transform.rotation);
		}

		// destroy game object
		if (ammoData.destroyOnImpact)
		{
			Destroy(gameObject);
		}
	}

	public void ApplyDamage(float damage)
	{

	}
}
