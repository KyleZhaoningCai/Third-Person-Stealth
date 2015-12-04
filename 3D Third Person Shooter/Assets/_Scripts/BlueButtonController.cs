/* Source File Name: BlueButtonController.cs
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

public class BlueButtonController : MonoBehaviour {

	// Public References
	public GameController gameController;
	
	// Private Instance Variables
	private AudioSource[] clips;
	// Use this for initialization
	void Start () {
		this.clips = gameObject.GetComponents<AudioSource> ();
	}
	


	// When the player is within the trigger, if the player has code3, blue lock is 
	// unlocked, otherwise lock up all locks
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				if(gameController._HasCode3){
					this.clips[0].Play();	
					gameController.UnlockBlueLock();
					this.gameController.infoLabel.text = "Blue Exit Lock is Lifted";
					this.gameController.Displaying();
				}else{
					this.clips[1].Play();
					gameController.ResetLocks();
					this.gameController.infoLabel.text = "Wrong Code or No Code\nAll Locks Reset";
					this.gameController.Displaying();
				}
			}
		}
	}
}
