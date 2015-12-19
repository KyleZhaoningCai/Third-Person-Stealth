/* Source File Name: RedButtonDeactivation.cs
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

public class RedButtonDeactivation : MonoBehaviour {

	public GameController gameController;

	void OnTriggerStay (Collider other) {
		if (other.CompareTag ("Player")) {
			if ( CrossPlatformInputManager.GetButtonDown("Fire1")){
				this.gameController.DeactivateSound();
				this.gameController.PressRedButton();
			}
		}
	}
}
