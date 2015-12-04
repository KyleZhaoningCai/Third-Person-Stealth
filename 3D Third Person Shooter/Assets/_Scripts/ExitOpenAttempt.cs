/* Source File Name: ExitOpenAttempts.cs
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

public class ExitOpenAttempt : MonoBehaviour {

	// Public references +++++++++++++++++++++++++++++++++++++
	public GameController gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// When the player is within the dungeon gate collision, attempt to open the gate
	// should alert the player that the gate cannot be opened this way
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
				this.gameController.infoLabel.text = "Exit Can't Be Opened From Here";
				this.gameController.Displaying();
			}
		}
	}
}
