/* Source File Name: TextGone.cs
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

public class TextGone : MonoBehaviour {

	// Public Instance Variables +++++++++++++++++++++++
	public GameController gameController;

	// When the plyer leaves the starting area, the text disappears
	void OnTriggerExit(Collider other){
		if (other.CompareTag ("Player")) {
			gameController.infoLabel.text = "";
		}
	}
}
