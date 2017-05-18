using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{

	[SerializeField]
	private bool isVRPlayer = false;
	public bool IsVRPlayer { get { return isVRPlayer; } }

	[SerializeField]
	private int hitsToKill = 10;

	[SerializeField]
	private GameObject destroyThisWhenKilled;

	[SerializeField]
	private GameObject hitEffectPrefab;
	[SerializeField]
	private GameObject deathEffectPrefab;

	[SerializeField]
	private bool doShakeOnHit = true;

	public GameObject persistentPrefabOnDeath;

	private int hitsTaken = 0;

	public void GetHit(ProjectileController hitBy)
	{
		Instantiate(hitEffectPrefab, hitBy.transform.position, hitBy.transform.rotation);

		if (doShakeOnHit)
		{
			Camera.main.Shake();
		}

		//-1 hits to kill means it is invincible
		if (hitsToKill > -1)
		{
			hitsTaken += hitBy.Damage;
			if (hitsTaken >= hitsToKill)
			{
				Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
				if(persistentPrefabOnDeath)Instantiate(persistentPrefabOnDeath, transform.position, transform.rotation); //if there is one, spawn in

				if (destroyThisWhenKilled != null)
				{
					Destroy(destroyThisWhenKilled);
				}

				Destroy(this);
			}
		}
		
	}

}
