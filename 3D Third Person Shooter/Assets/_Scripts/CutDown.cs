/* Source File Name: CutDown.cs
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

public class CutDown : MonoBehaviour {
	
	public GameObject Fallen;
	public GameController gameController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				this.gameController.PlayCutSound();
				this.gameController.infoLabel.text = "You Cut it Down";
				this.gameController.Displaying();
				Destroy (this.gameObject);
				Instantiate(Fallen, this.transform.position, this.transform.rotation);
			}
		}
	}
}
