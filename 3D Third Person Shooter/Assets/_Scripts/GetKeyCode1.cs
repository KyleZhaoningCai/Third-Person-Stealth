/* Source File Name: GetKeyCode1.cs
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

public class GetKeyCode1 : MonoBehaviour {

	// Public References
	public GameController gameController;
	
	// Private Instance Variables
	private AudioSource clip;

	// Use this for initialization
	void Start () {
		this.clip = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the player is within the trigger, pick up the key code1.
	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				gameController.GotCode1();
				this.clip.Play();
				this.gameController.infoLabel.text = "18026 Code Obtained\nLost Other Codes";
				this.gameController.Displaying();
			}
		}
	}
}
