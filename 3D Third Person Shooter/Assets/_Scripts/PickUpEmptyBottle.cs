/* Source File Name: PickUpEmptyBottle.cs
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

public class PickUpEmptyBottle : MonoBehaviour {
	
	// Public References
	public GameController gameController;
	
	// If the player is within the trigger, the player can pick up the empty bottle
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				this.gameController.PlayKeySound();
				this.gameController.ObtainEmptyBottle();
				this.gameController.infoLabel.text = "You Picked Up\nan Empty Bottle";
				this.gameController.Displaying();
				Destroy(this.gameObject);
			}
		}
	}
}
