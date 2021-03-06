﻿using System.Collections;
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
  private ProjectileController bigProjectile;

	[SerializeField]
	private AudioClip shootSound;
	[SerializeField]
	private AudioClip bigShootSound;
	[SerializeField]
	private AudioClip shieldCreateSound;

	[SerializeField]
	private Transform spawnLocation;

  [SerializeField] private GameObject shieldPrefab;

  [Header("Fire Rates")]
  public float fireTime = 0.1f;
  [SerializeField] private float fireCooldown;

  [SerializeField] private float bigBulletCooldownLength = 1f;
  [SerializeField] private float bigBulletCooldown = 0f;

  [SerializeField] private float shieldCooldownTime = 5f;
  [SerializeField] private float shieldCooldown = 0f;

  [SerializeField] private Transform chargeTransform;
  public float chargeSize = 0.25f;
  [SerializeField] private Transform shieldChargeTransform;
  public float shieldChargeSize = 0.3f;

	
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

      if (bigBulletCooldown > 0f) { bigBulletCooldown -= Time.deltaTime; }
      if (fireCooldown > 0f) { fireCooldown -= Time.deltaTime; }
      if (shieldCooldown > 0f) { shieldCooldown -= Time.deltaTime; }

      float currentBigBulletCharge = Mathf.Abs( (bigBulletCooldown / bigBulletCooldownLength) - 1f ) * chargeSize;
      chargeTransform.localScale = new Vector3( currentBigBulletCharge, currentBigBulletCharge, 1f );

      float currentShieldCharge = Mathf.Abs( (shieldCooldown / shieldCooldownTime) - 1f ) * shieldChargeSize;
      shieldChargeTransform.localScale = new Vector3( currentShieldCharge, currentShieldCharge, 1f );

      //bullets
      if(Input.GetButton("Fire1_Tank" + _tankIndex)){
        if(fireCooldown <= 0f){
				bool isBigBullet = (bigBulletCooldown <= 0f);

				ProjectileController projectile = Instantiate<ProjectileController>(
					isBigBullet ? bigProjectile : projectileToSpawn,
					spawnLocation.position,
					spawnLocation.rotation);

				AudioManager.Instance.PlaySound3D(
						isBigBullet ? bigShootSound : shootSound,
						transform.position, 1f,
						Random.Range(0.8f, 1.1f)
					);

				fireCooldown = fireTime;
				bigBulletCooldown = bigBulletCooldownLength;
			}
      }
      //shield
      if(Input.GetButton("Fire2_Tank" + _tankIndex)){
        if(shieldCooldown <= 0f){
          Instantiate(shieldPrefab, transform.position, shieldPrefab.transform.rotation);
				AudioManager.Instance.PlaySound3D(shieldCreateSound, transform.position, 1f, Random.Range(0.8f, 1.1f));
          shieldCooldown = shieldCooldownTime;
        }
      }
	}
  public Vector3 RoundToCardinal(Vector3 myVec){
    return new Vector3(Mathf.Round(myVec.x),Mathf.Round(myVec.y),Mathf.Round(myVec.z));
  }
}
