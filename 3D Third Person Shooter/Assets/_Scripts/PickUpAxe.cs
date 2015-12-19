/* Source File Name: PickUpAxe.cs
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

public class PickUpAxe : MonoBehaviour {

	// Public References
	public GameController gameController;

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
				this.gameController.PlayKeySound();
				this.gameController.PickUpAxe ();
				this.gameController.infoLabel.text = "You Picked Up an Axe";
				this.gameController.Displaying();
				Destroy (this.gameObject);

			}
		}
	}
}
