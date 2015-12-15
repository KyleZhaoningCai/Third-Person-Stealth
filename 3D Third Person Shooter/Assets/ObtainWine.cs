/* Source File Name: ObtainWine.cs
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

public class ObtainWine : MonoBehaviour {

	// Private Instance Variables
	private GameObject[] wines;
	private int wineCount = 0;
	private bool wineAvailable = true;


	// Public References
	public GameController gameController;

	// Reference
	void Start(){
		this.wines = GameObject.FindGameObjectsWithTag ("Wine");
	}

	// Allow the player to take a wine only once every half a second.
	void Update(){
		if (this.gameController._OffCooldown == false){
			this.gameController.ActionCoolDown();
		}
	}

	// If the player is within the trigger, the player can obtain a bottle of wine containing the
	// obtained liquid
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (this.gameController._OffCooldown){
				if (CrossPlatformInputManager.GetButtonDown("Fire1")){
					if (!this.gameController._HasWine){
						if (this.gameController._HasPoison){
							this.gameController.ObtainWine();
							this.TakeWine();
							if (this.wineAvailable){
								this.gameController.infoLabel.text = "You Obtained Poisoned Wine";
								this.gameController.Displaying();
								this.gameController.EnterCoolDown();
							}
						}else if (this.gameController._HasSleepingPill){
							this.gameController.ObtainWine();
							this.TakeWine();
							if (this.wineAvailable){
								this.gameController.infoLabel.text = "You Obtained Drugged Wine";
								this.gameController.Displaying();
								this.gameController.EnterCoolDown();
							}
						}else if (this.gameController._HasWater){
							this.gameController.ObtainWine();
							this.TakeWine();
							if (this.wineAvailable){
								this.gameController.infoLabel.text = "You Obtained Diluted Wine";
								this.gameController.Displaying();
								this.gameController.EnterCoolDown();
							}
						}else{
							this.gameController.infoLabel.text = "There Are Some Wine";
							this.gameController.Displaying();
						}
					}else{
						this.gameController.infoLabel.text = "You Already Have Wine";
						this.gameController.Displaying();
					}
				}

			}
		}
	}

	// The wine in the box is taken, and when the wine run out game is over
	private void TakeWine(){
		if (this.wineCount < this.wines.Length) {
			Destroy (this.wines [wineCount].gameObject);
			this.wineCount++;
		} else {
			this.gameController.GameOver("Out of Resources\nPress Any Key to Continue");
			this.wineAvailable = false;
		}
	}
}
