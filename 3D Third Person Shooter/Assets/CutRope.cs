/* Source File Name: CutRope.cs
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
public class CutRope : MonoBehaviour {

	// Public Instance Variables
	public GameController gameController;

	// Private Instance Variables
	private Rigidbody chandelierRigidbody;

	// Use this for initialization
	void Start () {
		this.chandelierRigidbody = GameObject.FindGameObjectWithTag ("Chandelier").GetComponent<Rigidbody> ();
	}

	// Allow the player to cut the rope if the player has axe, and chandelier will fall
	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown ("Fire1")) {
				this.gameController.infoLabel.text = "You Cut the Rope";
				this.gameController.Displaying();
				Destroy (this.gameObject);
				this.chandelierRigidbody.isKinematic = false;
				this.chandelierRigidbody.useGravity = true;
			}
		}
	}
}
