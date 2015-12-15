/* Source File Name: DestroyUponContact.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;

public class DestroyUponContact : MonoBehaviour {

	// Public Instance Variables
	public GameController gameController;

	// Private Instance Variables
	private GameObject[] HallGates;
	private float gameOverCountDown = 3f;
	private bool gameOver = false;

	// Reference
	void Start(){
		this.HallGates = GameObject.FindGameObjectsWithTag ("HallGate");
	}
	void Update(){

	}

	// After the chandelier falls, it causes a small explosion unless it hits the robot enemy
	// in which case it causes a huge explosion which destroy the lazer and the gate.
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("ExplosionEnemy")) {
			this.gameController.ChandelierExplode (true);
			Destroy (this.gameObject);
			for (int i = 0; i < HallGates.Length; i++){
				Destroy (HallGates[i].gameObject);
			}
			Destroy (other.gameObject);
		} else if(other.CompareTag("Enemy")){
		}else {
			this.gameController.ChandelierExplode (false);
			this.gameController.DelayGameOver();
			Destroy (this.gameObject);

		}

	}
}
