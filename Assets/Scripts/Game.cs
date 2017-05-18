using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

	[SerializeField]
	private DamageableObject[] objectsOnVRPlayer;
	[SerializeField]
	private DamageableObject[] objectsOnGroundPlayer;


	private bool gameIsRunning = true;

	[SerializeField]
	private GameObject activateOnVRVictory;
	[SerializeField]
	private GameObject activateOnGroundVictory;

	private void Update()
	{
		if (gameIsRunning)
		{
			bool anyVRPlayerExists = false;
			for (int i=0; i<objectsOnVRPlayer.Length; i++)
			{
				//really dumb way to check if the hitboxes have been destroyed yet
				if (objectsOnVRPlayer[i] != null) { anyVRPlayerExists = true; break; }
			}

			bool anyGroundPlayerExists = false;
			for (int i=0; i<objectsOnGroundPlayer.Length; i++)
			{
				if (objectsOnGroundPlayer[i] != null) { anyGroundPlayerExists = true;break; }
			}

			if (!anyGroundPlayerExists)
			{
				gameIsRunning = false;
				activateOnVRVictory.SetActive(true);
			}
			else
			{
				if (!anyVRPlayerExists)
				{
					gameIsRunning = false;
					activateOnGroundVictory.SetActive(true);
				}
			}
		}
	}

}
