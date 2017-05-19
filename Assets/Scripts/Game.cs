using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	private enum VictoryTypes
	{
		DestroyTanks = 0,
		DestroyAllBuildings = 1,
		DestroyTanksAndAllBuildings = 2
	}

	[SerializeField]
	private VictoryTypes victoryType = VictoryTypes.DestroyTanks;

	[SerializeField]
	private DamageableObject[] objectsOnVRPlayer;
	[SerializeField]
	private DamageableObject[] objectsOnGroundPlayer;


	private bool gameIsRunning = true;

	[SerializeField]
	private GameObject activateOnVRVictory;
	[SerializeField]
	private GameObject activateOnGroundVictory;

	[SerializeField]
	private CityGeneration cityGenerator;

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

			if (victoryType == VictoryTypes.DestroyTanks || victoryType == VictoryTypes.DestroyTanksAndAllBuildings)
			{
				for (int i = 0; i < objectsOnGroundPlayer.Length; i++)
				{
					if (objectsOnGroundPlayer[i] != null) { anyGroundPlayerExists = true; break; }
				}
			}
			if (victoryType == VictoryTypes.DestroyAllBuildings || victoryType == VictoryTypes.DestroyTanksAndAllBuildings)
			{
				for (int i=0; i<cityGenerator.BuildingHitboxes.Count; i++)
				{
					if (cityGenerator.BuildingHitboxes[i] != null) { anyGroundPlayerExists = true; break; }
				}
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

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}

}
