/* Source File Name: CameraMovement.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Public Instance Variables +++++++++++++++++++++++
	public float smooth = 1.5f;

	// Private Instance Variables ++++++++++++++++++++++
	private Transform player;
	private Vector3 relCameraPos;
	private float relCameraPosMag;
	private Vector3 newPos;

	// References
	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	// Check if the player is at the center of the view. If not, move the 
	// camera to place the player in the center of the view.
	void FixedUpdate(){
		Vector3 standardPos = player.position + relCameraPos;
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;
		Vector3[] checkPoints = new Vector3[5];
		checkPoints [0] = standardPos;
		checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f);
		checkPoints [2] = Vector3.Lerp (standardPos, abovePos, 0.5f);
		checkPoints [3] = Vector3.Lerp (standardPos, abovePos, 0.75f);
		checkPoints [4] = abovePos;

		for (int i = 0; i < checkPoints.Length; i++) {
			if (ViewingPosCheck(checkPoints[i])){
				break;
			}
		}

		transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime);
		SmoothLlookAt ();
	}

	// Check if the player is hit by the Raycast
	bool ViewingPosCheck(Vector3 checkPos){
		RaycastHit hit;

		if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCameraPosMag)) {
			if (hit.transform != player){
				return false;
			}
		}
		newPos = checkPos;
		return true;
	}

	// Make the camera movement smooth
	void SmoothLlookAt(){
		Vector3 relPlayerPosition = player.position - transform.position;
		Quaternion lookAtRotation = Quaternion.LookRotation (relPlayerPosition, Vector3.up);
		transform.rotation = Quaternion.Lerp (transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}
}
