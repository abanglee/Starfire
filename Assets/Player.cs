﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	[Header("General")]
	[Tooltip("In ms^-1")][SerializeField] float controlSpeed = 20f;
	[Tooltip("In m")][SerializeField] float xRange = 10f;
	[Tooltip("In m")][SerializeField] float yRange = 9f;
	[SerializeField] GameObject[] guns;

	[Header("Screen-position Based")]
	[SerializeField] float positionPitchFactor = -4f;
	[SerializeField] float positionYawFactor = 3f;

	[Header("Control-throw Based")]
	[SerializeField] float controlPitchFactor = -5f;
	[SerializeField] float controlRollFactor = -5f;

	float xThrow, yThrow;
	bool isControlsEnabled = true;


    // Update is called once per frame
    void Update()
    {
		if(isControlsEnabled){
			ProcessTranslation();
			ProcessRotation();
			ProcessFiring();
		}
	}

	void OnPlayerDeath(){

		print("Freeze controls.");
		isControlsEnabled = false;
	}


	private void ProcessRotation(){

		float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
		float pitchDueToControlThrow = yThrow * controlPitchFactor;
		float pitch = pitchDueToPosition + pitchDueToControlThrow;

		float yaw = transform.localPosition.x * positionYawFactor;

		float roll = xThrow * controlRollFactor;

		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}

	private void ProcessTranslation(){
		
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");

		float xOffset = xThrow *controlSpeed * Time.deltaTime;
		float yOffset = yThrow *controlSpeed * Time.deltaTime;

		float rawXPos = transform.localPosition.x + xOffset;
		float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

		float rawYPos = transform.localPosition.y + yOffset;
		float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

		transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
	}
		
	void ProcessFiring(){
		if(CrossPlatformInputManager.GetButton("Fire")){
			ActivateGuns();
		}
		else{
			DeactivateGuns();
		}
	}

	private void ActivateGuns(){
		foreach(GameObject gun in guns){
			gun.SetActive(true);
		}
	}

	private void DeactivateGuns(){
		foreach(GameObject gun in guns){
			gun.SetActive(false);
		}
	}
}
