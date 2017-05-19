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

	[SerializeField]
	private AudioClip shootSound;
	[SerializeField]
	private AudioClip bigShootSound;

	private float bigBulletCooldown = 0f;

	private SteamVR_TrackedObject controller;

	[Header("Fire Rates")]
	public float fireTime = 0.1f;
 	[SerializeField] private float fireCooldown;

	private void Awake()
	{
		controller = GetComponent<SteamVR_TrackedObject>();
	}

	private void Update()
	{
		if (fireCooldown > 0f) { fireCooldown -= Time.deltaTime; }
		if (bigBulletCooldown > 0f) { bigBulletCooldown -= Time.deltaTime; }

#if UNITY_EDITOR
		if (Input.GetKey(KeyCode.Space))
		{
			Fire();
		}
#else
		var device = SteamVR_Controller.Input((int)controller.index);
		
		if (device != null)
		{
			if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
			{
				Fire();
			}
		}
		
#endif
	}

	private void Fire()
	{
		if (fireCooldown <= 0f)
		{
			bool isBigBullet = (bigBulletCooldown <= 0f);

		ProjectileController projectile = Instantiate<ProjectileController>(
			isBigBullet ? bigProjectile : projectileToSpawn,
			spawnLocation.position,
			spawnLocation.rotation);

		AudioManager.Instance.PlaySound3D(
				isBigBullet ? bigShootSound : shootSound,
				transform.position, 1f,
				Random.Range(0.8f, 1.1f)
			);

			fireCooldown = fireTime;
			bigBulletCooldown = bigBulletCooldownLength;
		}
	}

}
