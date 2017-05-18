using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandShipController : MonoBehaviour
{

	[SerializeField]
	private ProjectileController projectileToSpawn;
	[SerializeField]
	private ProjectileController bigProjectile;
	[SerializeField]
	private Transform spawnLocation;

	[SerializeField]
	private float bigBulletCooldownLength = 1f;

	private float bigBulletCooldown = 0f;

	private SteamVR_TrackedObject controller;

	private void Awake()
	{
		controller = GetComponent<SteamVR_TrackedObject>();
	}

	private void Update()
	{
		if (bigBulletCooldown > 0f) { bigBulletCooldown -= Time.deltaTime; }

#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Fire();
		}
#else
		var device = SteamVR_Controller.Input((int)controller.index);
		
		if (device != null)
		{
			if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
			{
				Fire();
			}
		}
		
#endif
	}

	private void Fire()
	{
		ProjectileController projectile = Instantiate<ProjectileController>(
			bigBulletCooldown <= 0f ? bigProjectile : projectileToSpawn,
			spawnLocation.position,
			spawnLocation.rotation);

		bigBulletCooldown = bigBulletCooldownLength;
	}

}
