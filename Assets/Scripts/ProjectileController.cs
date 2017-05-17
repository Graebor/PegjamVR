using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

	[SerializeField]
	private Rigidbody rb;
	[SerializeField]
	private float launchForceAmount = 10f;


	public void Launch()
	{
		rb.AddForce(transform.forward * launchForceAmount);
	}

}
