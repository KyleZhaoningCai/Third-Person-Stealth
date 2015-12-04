/* Source File Name: Deactivate.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class Deactivate : MonoBehaviour {

	// Private Instance Variables +++++++++++++++++++++++++++++
	private Collider[] colliders;
	private Collider bigCollider;
	private Collider smallCollider;
	private AudioSource clip;

	// Public Instance Variables and References +++++++++++++++
	public GameObject enemy;
	public Animator _animator;
	public NavMeshAgent navMeshAgent;
	public GameController gameController;

	// References
	void Start(){
		colliders = enemy.GetComponentsInChildren<Collider> ();
		bigCollider = colliders[0];
		smallCollider = colliders [1];
		this.clip = gameObject.GetComponent<AudioSource> ();
	}

	// When the player is within the trigger, if the player can deactivate the enemy
	// and can pick up enemy's key code
	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				this.clip.Play ();
				bigCollider.enabled = false;
				smallCollider.enabled = true;
				navMeshAgent.Stop();
				this._animator.SetInteger("AnimeState", 2);
				this.gameController.infoLabel.text = "You Used Deactivation Terminal\nEnemy Not Affected If Attacking";
				this.gameController.Displaying();


			}
		}
	}
}
