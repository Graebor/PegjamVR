﻿using System.Collections;
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
	private AudioClip customDieSound;

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

		AudioManager.Instance.PlaySound3D("TankBullet", transform.position, 1f, Random.Range(0.8f, 1.2f));

		//-1 hits to kill means it is invincible
		if (hitsToKill > -1)
		{
			hitsTaken += hitBy.Damage;
			if (hitsTaken >= hitsToKill)
			{
				Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
				if(persistentPrefabOnDeath)Instantiate(persistentPrefabOnDeath, transform.position, transform.rotation); //if there is one, spawn in

				if (customDieSound != null)
				{
					AudioManager.Instance.PlaySound3D(customDieSound, transform.position, 1f, Random.Range(0.8f, 1.2f));
				}
				else
				{
					AudioManager.Instance.PlaySound3D("DestroyBuilding", transform.position, 1f, Random.Range(0.8f, 1.2f));
				}

				if (destroyThisWhenKilled != null)
				{
					Destroy(destroyThisWhenKilled);
				}

				Destroy(this);
			}
		}
		
	}

}
