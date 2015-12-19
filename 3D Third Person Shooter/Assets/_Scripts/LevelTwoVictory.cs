/* Source File Name: LevelTwoVictory.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using System;
using UnityEngine;
using System.Collections;

public class LevelTwoVictory : MonoBehaviour {

	// Public References ++++++++++++++++++++++++++++++++++
	public GameController gameController;
	
	// Private Instance Variables +++++++++++++++++++++++++
	private SceneFadeInOut sceneFadeInOut;
	
	// Use this for initialization
	void Start () {
		sceneFadeInOut = GameObject.FindGameObjectWithTag ("Fader").GetComponent<SceneFadeInOut> ();
	}
	
	// When the player stays in the victory trigger, the screen fades out and level one is won.
	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			this.gameController.infoLabel.text = "You've Passed Level Two!";
			sceneFadeInOut.EndScene();
		}
	}
}
