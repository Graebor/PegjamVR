using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

	[SerializeField]
	private float moveSpeed = 1f;

	[SerializeField]
	private bool isVRPlayer = true;
	public bool IsVRPlayer { get { return isVRPlayer; } }


	private void Update()
	{
		transform.position += (transform.forward * Time.deltaTime * moveSpeed);

		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, transform.forward, out hitInfo, moveSpeed * Time.deltaTime * 2f))
		{
			DamageableObject dmg = hitInfo.collider.gameObject.GetComponent<DamageableObject>();
			if (dmg != null)
			{
				if (dmg.IsVRPlayer != isVRPlayer)
				{
					Camera.main.Shake();

					dmg.GetHit(this);
					Destroy(this.gameObject);
				}
			}
		}
	}

}
