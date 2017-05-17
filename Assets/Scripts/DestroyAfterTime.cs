using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

	[SerializeField]
	private float lifetime = 3f;

	private float awakeTime;

	private void Awake()
	{
		awakeTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (Time.realtimeSinceStartup >= awakeTime + lifetime)
		{
			Destroy(this.gameObject);
		}
	}

}
