using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
	
	[Tooltip("in seconds")][SerializeField] float levelLoadDelay = 1f;
	[Tooltip("FX Prefab on player")][SerializeField] GameObject deathFX;
	
	void OnTriggerEnter(Collider other){

		print("Player triggered with an object.");
		StartDeathSequence();
		deathFX.SetActive(true);
		Invoke("ReloadScene", levelLoadDelay);
	}

	private void StartDeathSequence(){

		print("Player dying");
		SendMessage("OnPlayerDeath");

	}

	private void ReloadScene(){
		SceneManager.LoadScene(1);
	}
}
