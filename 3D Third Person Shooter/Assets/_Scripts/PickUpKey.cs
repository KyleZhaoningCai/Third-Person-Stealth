/* Source File Name: PickUpKey.cs
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

public class PickUpKey : MonoBehaviour {

	// Public References
	public GameController gameController;
		
	// If the player is within the trigger, the player can pick up the key
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				gameController.GotKey();
				gameController.PlayKeySound();
				this.gameController.infoLabel.text = "You Picked Up a Key";
				this.gameController.Displaying();
				Destroy(this.gameObject);
			}
		}
	}
}
