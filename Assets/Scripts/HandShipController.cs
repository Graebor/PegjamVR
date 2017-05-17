using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class HandShipController : MonoBehaviour
{

	[SerializeField]
	private GameObject projectileToSpawn;
	[SerializeField]
	private Transform spawnLocation;

	private SteamVR_TrackedObject controller;

	private void Awake()
	{
		controller = GetComponent<SteamVR_TrackedObject>();
	}

	private void Update()
	{
		var device = SteamVR_Controller.Input((int)controller.index);
		
		if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			GameObject projectile = Instantiate(projectileToSpawn);
			projectile.transform.position = spawnLocation.position;
			projectile.transform.rotation = spawnLocation.rotation;
		}
	}

}
