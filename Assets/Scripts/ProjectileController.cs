using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private float launchForceAmount = 10f;
	[SerializeField]
	private bool isVRPlayer = true;
	public bool IsVRPlayer { get { return isVRPlayer; } }

	public void Launch()
	{
		rb.AddForce(transform.forward * launchForceAmount);
	}

	private void OnCollisionEnter(Collision collision)
	{
		DamageableObject dmg = collision.collider.gameObject.GetComponent<DamageableObject>();
		if (dmg != null)
		{
			if (dmg.IsVRPlayer != isVRPlayer)
			{
				dmg.GetHit(this);
				Destroy(this.gameObject);
			}
		}
	}

}
