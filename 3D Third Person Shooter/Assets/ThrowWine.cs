/* Source File Name: ThrowWine.cs
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

public class ThrowWine : MonoBehaviour {

	// Public Instance Variables
	public GameObject[] wines;
	public Transform spawnPoint;
	public GameController gameController;

	
	// Different kinds of wine is thrown according to the kind of liquid on the player
	// Update is called once per frame
	void Update () {
		if (this.gameController._OffCooldown == false){
			this.gameController.ActionCoolDown();
		}else{
			if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
				if (this.gameController._GameOver == false){
					if (this.gameController._HasWine && this.gameController._HasPoison) {
						Instantiate (wines [0], this.spawnPoint.position, this.spawnPoint.rotation);
						this.gameController.LoseWine ();
					}
					if (this.gameController._HasWine && this.gameController._HasSleepingPill) {
						Instantiate (wines [1], this.spawnPoint.position, this.spawnPoint.rotation);
						this.gameController.LoseWine ();
					}
					if (this.gameController._HasWine && this.gameController._HasWater) {
						Instantiate (wines [2], this.spawnPoint.position, this.spawnPoint.rotation);
						this.gameController.LoseWine ();
					}
				}
			}
		}
	}
}
