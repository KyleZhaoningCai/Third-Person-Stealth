/* Source File Name: SceneFadeInOut.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour {

	// Public Instance Variables +++++++++++++++++++++++++++
	public float fadeSpeed = 1.5f;
	public int nextLevel = 1;

	// Private Instance Variables ++++++++++++++++++++++++++
	private bool sceneStarting = true;
	private GUITexture _guiTexture;

	// References
	void Awake(){
		this._guiTexture = gameObject.GetComponent<GUITexture> ();
		this._guiTexture.pixelInset = new Rect (0f, 0f, Screen.width, Screen.height);
	}

	void Update(){
		// If the scene is starting, call StartScene method to fade in
		if (this.sceneStarting) {
			this.StartScene();
		}
	}

	// Fade in
	void FadeToClear(){
		this._guiTexture.color = Color.Lerp (this._guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	// Fade out
	void FadeToBlack(){
		this._guiTexture.color = Color.Lerp (this._guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	// Start the scene and fade in
	void StartScene(){
		FadeToClear ();

		if (this._guiTexture.color.a <= 0.05f) {
			this._guiTexture.color = Color.clear;
			this._guiTexture.enabled = false;
			this.sceneStarting = false;
		}
	}

	// End the scene and fade out
	public void EndScene(){
		this._guiTexture.enabled = true;
		FadeToBlack ();

		if (_guiTexture.color.a >= 0.95f) {
			Application.LoadLevel(this.nextLevel);
		}

	}
}
