/* Source File Name: FallBall1.cs
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

public class FallBall1 : MonoBehaviour {

	public float force;

	private Rigidbody _rigidbody;
	private Vector3 movement;
	private Collider[] sign1Colliders;

	// Use this for initialization
	void Start () {
		this._rigidbody = gameObject.GetComponent<Rigidbody> ();
		this.sign1Colliders = GameObject.FindGameObjectWithTag ("Sign1").GetComponents<Collider>();		
		this._rigidbody.AddTorque (-this.transform.right * this.force, ForceMode.Impulse);
		this.sign1Colliders[1].enabled = false;
	}

}
