/* Source File Name: SlideWine.cs
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

public class SlideWine : MonoBehaviour {

	// Private Instance Variables
	private Rigidbody _rigidbody;
	private Transform _transform;
	private Collider[] _colliders;
	private float timer = 1.5f;

	// Use this for initialization
	// Give the wine a push force;
	void Start () {
		this._transform = gameObject.GetComponent<Transform> ();
		this._rigidbody = gameObject.GetComponent<Rigidbody> ();
		this._rigidbody.AddForce(this._transform.forward * 400);
		this._colliders = gameObject.GetComponents<Collider> ();
	}

	// The wine will not move and has no collision after 1.5 seconds
	void Update(){
		if (this.timer > 0) {
			this.timer -= Time.deltaTime;
		} else {
			this._rigidbody.isKinematic = true;
			this._colliders[0].enabled = false;
		}
	}
	

}
