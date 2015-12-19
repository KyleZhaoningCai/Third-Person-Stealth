﻿/* Source File Name: BottleUpPoison.cs
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

public class BottleUpPoison : MonoBehaviour {

	// Public References
	public GameController gameController;

	private AudioSource clip;

	void Start(){
		this.clip = gameObject.GetComponent<AudioSource>();
	}
	// Allow the player to do a bottling action only once every half a second.
	void Update(){
		if (this.gameController._OffCooldown == false){
			this.gameController.ActionCoolDown();

		}
	}
	
	// If the player is within the trigger and has the empty bottle, the player can bottle up the poison liquid
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (this.gameController._OffCooldown){
				if (CrossPlatformInputManager.GetButtonDown("Fire1")){
					if (!this.gameController._HasWine){
						if (this.gameController._HasEmptyBottle){
							this.clip.Play ();
							if (this.gameController._HasWater || this.gameController._HasSleepingPill){
								this.gameController.BottleUpPoison();
								this.gameController.infoLabel.text = "You Swapped Your Liquid\nFor Toxic Liquid";
								this.gameController.Displaying();
								this.gameController.EnterCoolDown();
							}else{
								this.gameController.BottleUpPoison();
								this.gameController.infoLabel.text = "You Bottled Up Some Toxic Liquid";
								this.gameController.Displaying();
								this.gameController.EnterCoolDown();
							}
						}else{
							this.gameController.infoLabel.text = "A Flask Labaled\n'Toxic Liquid'";
							this.gameController.Displaying();
						}
					}else{
						this.gameController.infoLabel.text = "You Cannot Do This Action\nWith Wine in Your Hand";
						this.gameController.Displaying();
					}
				}
			}
		}
	}
}
