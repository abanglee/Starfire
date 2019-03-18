using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;
	[SerializeField] int scorePerHit = 12;
	[SerializeField] int hits = 4;

	ScoreBoard scoreBoard;
		
    // Start is called before the first frame update
    void Start()
    {
		Collider boxCollider = gameObject.AddComponent<BoxCollider>();
		boxCollider.isTrigger = false;

		scoreBoard = FindObjectOfType<ScoreBoard>();
    }
 
	void OnParticleCollision(GameObject other){
		scoreBoard.ScoreHit(scorePerHit);
			hits--;
			if (hits <= 0){
				KillEnemy();
			}
	}

	private void KillEnemy(){
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parent;
		Destroy(gameObject);
	}
}
