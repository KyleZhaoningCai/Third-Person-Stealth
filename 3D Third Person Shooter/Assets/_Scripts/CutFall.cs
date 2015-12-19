/* Source File Name: CutFall.cs
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

public class CutFall : MonoBehaviour {

	public int direction;
	public float force = 0;
	
	private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
		this._rigidbody = gameObject.GetComponent<Rigidbody> ();
		if (direction == 1) {
			this._rigidbody.AddTorque (-this.transform.right * this.force, ForceMode.Impulse);
		} else {
			this._rigidbody.AddTorque (-this.transform.forward * this.force, ForceMode.Impulse);
		}
	}

}
