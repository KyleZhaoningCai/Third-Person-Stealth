/* Source File Name: StartTimer.cs
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

public class StartTimer : MonoBehaviour {

	// Public Instance Variables+++++++++++++++
	public GameController gameController;


	// When the player exit the starting area, start the bomb timer
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			this.gameController.StartTimer();
			this.gameController.infoLabel.text = "";
		}
	}
}
