/* Source File Name: FallBall5.cs
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

public class FallBall5 : MonoBehaviour {

	public float force;

	private Rigidbody _rigidbody;
	private Vector3 movement; 
	private Collider[] Tree3Colliders;
	private Collider[] Tree4Colliders;

	// Use this for initialization
	void Start () {
		this._rigidbody = gameObject.GetComponent<Rigidbody> ();
		this._rigidbody.AddTorque (-this.transform.right * this.force, ForceMode.Impulse);
		this.Tree3Colliders = GameObject.FindGameObjectWithTag ("Tree3").GetComponents<Collider>();	
		this.Tree4Colliders = GameObject.FindGameObjectWithTag ("Tree4").GetComponents<Collider>();	
		this.Tree3Colliders[1].enabled = false;
		this.Tree4Colliders [1].enabled = false;
	}

}
