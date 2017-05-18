using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	public static CameraShake Instance;

	[SerializeField]
	private float shakeFalloffFactor = 1.1f;

	private float currentShake = 0f;


	private void Awake()
	{
		Instance = this;
	}

	public void Shake(float amt)
	{

	}

}
