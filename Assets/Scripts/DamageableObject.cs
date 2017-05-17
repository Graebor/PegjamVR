using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{

	[SerializeField]
	private bool isVRPlayer = false;
	public bool IsVRPlayer { get { return isVRPlayer; } }

	[SerializeField]
	private GameObject hitEffectPrefab;

	public void GetHit(ProjectileController hitBy)
	{
		Instantiate(hitEffectPrefab, hitBy.transform.position, hitBy.transform.rotation);
	}

}
