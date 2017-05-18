using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceWorldDirection : MonoBehaviour
{

	[SerializeField]
	private Vector3 desiredForwardVector = Vector3.forward;

	[SerializeField]
	private SteamVR_UpdatePoses poseUpdater;


	private void OnEnable()
	{
		if (poseUpdater != null)
		{
			poseUpdater.OnPosesUpdated += PoseUpdater_OnPosesUpdated;
		}
	}
	private void OnDisable()
	{
		if (poseUpdater != null)
		{
			poseUpdater.OnPosesUpdated -= PoseUpdater_OnPosesUpdated;
		}
	}

	private void PoseUpdater_OnPosesUpdated()
	{
		SetPose();
	}

	private void Update()
	{
		if (poseUpdater == null)
		{
			SetPose();
		}
		
	}

	private void SetPose()
	{
		transform.forward = desiredForwardVector.normalized;
	}

}
