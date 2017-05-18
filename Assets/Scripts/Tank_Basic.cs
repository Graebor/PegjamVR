using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_Basic : MonoBehaviour {

  // not the best way but ok for now. if false we're player 1, if true player two. 
  public bool isTank2 = false;

  // Input
  public Vector2 inputDirection = Vector2.zero;
  public bool isShootingActive = false;
  public bool isShieldingActive = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    GetInput();

    isShootingActive = Input.GetButton("Fire1_Tank1");
    Debug.Log(Input.GetKey(KeyCode.A) + " " + Input.GetKey(KeyCode.LeftArrow));
   
	}

  private void GetInput(){

    if(!isTank2){
      // Tank 1

      isShootingActive = Input.GetButton("Fire1_Tank1");
      isShieldingActive = Input.GetButton("Fire2_Tank1");
      inputDirection.x = Input.GetAxis("Horizontal_Tank1");
      inputDirection.y = Input.GetAxis("Vertical_Tank1");

    }else{
      // is tank 2
      isShootingActive = Input.GetButton("Fire1_Tank2");
      isShieldingActive = Input.GetButton("Fire2_Tank2");
      inputDirection.x = Input.GetAxis("Horizontal_Tank2");
      inputDirection.y = Input.GetAxis("Vertical_Tank2");
    }
  }
}
