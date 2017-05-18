using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandShipController : MonoBehaviour
{

	[SerializeField]
	private ProjectileController projectileToSpawn;
	[SerializeField]
	private Transform spawnLocation;

	private SteamVR_TrackedObject controller;

	private void Awake()
	{
		controller = GetComponent<SteamVR_TrackedObject>();
	}

	private void Update()
	{
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
		ProjectileController projectile = Instantiate<ProjectileController>(projectileToSpawn);
		projectile.transform.position = spawnLocation.position;
		projectile.transform.rotation = spawnLocation.rotation;
	}

}
