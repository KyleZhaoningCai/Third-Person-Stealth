/* Source File Name: OpenJailDoor.cs
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

public class OpenJailDoor : MonoBehaviour {

	// Private Instance Variables ++++++++++++++++++++++
	private Vector3 doorOpenPosition;
	private Transform _transform;
	private AudioSource clip;

	// Public References +++++++++++++++++++++++++++++++
	public GameController gameController;

	// Use this for initialization
	void Start () {
		doorOpenPosition = new Vector3 (1.85f, -0.1f, -0.15f);
		this.clip = gameObject.GetComponent<AudioSource> ();
		this._transform = gameObject.GetComponent<Transform> ();
	}

	// If the player is within the trigger, the player can open the jail door if
	// the player has the key, otherwise alert the player that the door is locked
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				if (this.gameController._HasKey){
					this._transform.position = this.doorOpenPosition;
					this.clip.Play();
					this.gameController.infoLabel.text = "You Opened the Jail Gate";
					this.gameController.Displaying();
				}else{
					this.gameController.infoLabel.text = "The Jail Gate is Locked";
					this.gameController.Displaying();
				}
			}
		}
	}

}
