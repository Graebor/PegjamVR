using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Basic : MonoBehaviour {
  [Range(0,1)] 
  public int tankIndex = 0;

  private string _tankIndex{
    get{
      return (tankIndex + 1).ToString();
    }
  }

  // Input
  public Vector3 input;
  public float inputMag;
  public float inputSensitivity = 1f;

  public Transform pivot;
  public Vector3 look;
  public Vector3 move;
  public float moveSpeed = 8f;
  public float turnSpeed = 8f;
  public float accelSpeed = 5f;
  public float decelSpeed = 8f;

	[SerializeField]
	private ProjectileController projectileToSpawn;
	[SerializeField]
	private Transform spawnLocation;


	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
      input = new Vector3(Input.GetAxisRaw("Horizontal_Tank" + _tankIndex), 0f, Input.GetAxisRaw("Vertical_Tank" + _tankIndex)) * inputSensitivity;
      inputMag = Mathf.Clamp01(input.magnitude);
      if(inputMag > 0f){
        look = RoundToCardinal(input.normalized).normalized;
      }

      //float myInputAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg;
      pivot.localEulerAngles = new Vector3(0,Mathf.LerpAngle(pivot.localEulerAngles.y, Mathf.Atan2(look.x, look.z) * Mathf.Rad2Deg, turnSpeed * Time.deltaTime),0);

      float myLerpSpeed = inputMag == 0f ? decelSpeed : accelSpeed; //how fast to lerp
      move = Vector3.Lerp(move,RoundToCardinal(input.normalized).normalized * moveSpeed * inputMag,Time.deltaTime * myLerpSpeed);

      transform.position += move * Time.deltaTime;

      if(Input.GetButtonDown("Fire1_Tank" + _tankIndex)){

			ProjectileController projectile = Instantiate<ProjectileController>(projectileToSpawn);
			projectile.transform.position = spawnLocation.position;
			projectile.transform.rotation = spawnLocation.rotation;

			//Debug.Log("Pew pew I am tank " + _tankIndex);
      }
      if(Input.GetButton("Fire2_Tank" + _tankIndex)){
        Debug.Log("I am shielding........ also I am tank " + _tankIndex);
      }
	}
  public Vector3 RoundToCardinal(Vector3 myVec){
    return new Vector3(Mathf.Round(myVec.x),Mathf.Round(myVec.y),Mathf.Round(myVec.z));
  }
}
