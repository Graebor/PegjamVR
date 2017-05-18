﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceWorldDirection : MonoBehaviour
{

	[SerializeField]
	private Vector3 desiredForwardVector = Vector3.forward;


	private void Update()
	{
		transform.forward = desiredForwardVector.normalized;
	}

}
