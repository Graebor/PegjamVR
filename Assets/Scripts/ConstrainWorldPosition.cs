using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainWorldPosition : MonoBehaviour
{
	[SerializeField]
	private Transform goalPosition;
	[SerializeField]
	private Vector3 goalPositionOffset;
	[SerializeField]
	private Vector3 minWorldPositionValues;
	[SerializeField]
	private Vector3 maxWorldPositionValues;
	[SerializeField]
	private float followSpeed;


	private void Update()
	{
		Vector3 newPos = goalPosition.position + goalPositionOffset;

		newPos.x = Mathf.Clamp(newPos.x, minWorldPositionValues.x, maxWorldPositionValues.x);
		newPos.y = Mathf.Clamp(newPos.y, minWorldPositionValues.y, maxWorldPositionValues.y);
		newPos.z = Mathf.Clamp(newPos.z, minWorldPositionValues.z, maxWorldPositionValues.z);

		transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * followSpeed);
	}

}
