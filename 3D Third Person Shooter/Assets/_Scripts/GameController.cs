/* Source File Name: GameController.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class GameController : MonoBehaviour {

	// Private Instance Variables +++++++++++++++++++++++
	private int lives = 3;
	private bool hasKey;
	private bool hasCode1;
	private bool hasCode2;
	private bool hasCode3;
	private bool redLock;
	private bool blueLock;
	private bool greenLock;
	private Vector3 doorOpenPosition;
	private bool gameOver;
	private GameObject player;
	private Renderer[] playerRenderer;
	private float displayTimer = 2f;
	private bool displaying = false;
	private AudioSource[] clips;

	// Public References ++++++++++++++++++++++++++++++++
	public GameObject explosion;
	public GameObject gate;
	public Text lifeLabel;
	public Text infoLabel;

	// Public Read-Only
	public bool _HasKey {
		get {
			return this.hasKey;
		}
	}

	public bool _HasCode3 {
		get {
			return this.hasCode3;
		}
	}

	public bool _HasCode1 {
		get {
			return this.hasCode1;
		}
	}

	public bool _HasCode2 {
		get {
			return this.hasCode2;
		}
	}

	public bool _RedLock {
		get {
			return this.redLock;
		}
	}
	public bool _BlueLock {
		get {
			return this.blueLock;
		}
	}
	public bool _GreenLock {
		get {
			return this.greenLock;
		}
	}

	// Use this for initialization
	void Start () {
		hasKey = false;
		hasCode1 = false;
		hasCode2 = false;
		hasCode3 = false;
		gameOver = false;
		ResetLocks ();
		doorOpenPosition = new Vector3 (-2.74f, 1.5f, 12.98f);
		this.UpdateLifeLabel();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerRenderer = player.GetComponentsInChildren<Renderer> ();
		clips = gameObject.GetComponents<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
		// If the game is over, allow player to press any key to restart
		if (gameOver) {
			if (Input.anyKeyDown){
				Application.LoadLevel(0);
				gameOver = false;
			}
		}
		// If the info text is displaying, delete the text after two seconds
		if (this.displaying) {
			this.displayTimer -= Time.deltaTime;
			if(this.displayTimer <= 0){
				ClearInfoText();
			}
		}
	}

	// Player obtains the key and hasKey becomes true
	public void GotKey(){
		this.hasKey = true;
	}

	// Player obtains code1 and loses the other code
	public void GotCode1(){
		this.hasCode1 = true;
		this.hasCode2 = false;
		this.hasCode3 = false;
	}

	// Player obtains code2 and loses the other code
	public void GotCode2(){
		this.hasCode2 = true;
		this.hasCode1 = false;
		this.hasCode3 = false;
	}

	// Player obtains code3 and loses the other code
	public void GotCode3(){
		this.hasCode3 = true;
		this.hasCode1 = false;
		this.hasCode2 = false;
	}

	// Player loses a life, if player has no more life, game is over
	public void ReduceLives(){
		lives -= 1;
		this.UpdateLifeLabel();
		if (this.lives <= 0) {
			this.GameOver();
		}
	}

	// Player unlocks the red lock and check if the dungeon gate can be opened
	public void UnlockRedLock(){
		this.redLock = true;
		this.OpenGate ();
	}

	// Player unlocks the blue lock and check if the dungeon gate can be opened
	public void UnlockBlueLock(){
		this.blueLock = true;
		this.OpenGate ();
	}

	// Player unlocks the green lock and check if the dungeon gate can be opened
	public void UnlockGreenLock(){
		this.greenLock = true;
		this.OpenGate ();
	}

	// Locks up all dungeon locks again
	public void ResetLocks(){
		this.redLock = false;
		this.blueLock = false;
		this.greenLock = false;
	}

	// If all 3 locks are unlocked, open the dungeon gate
	public void OpenGate(){
		this.clips [2].Play ();
		if (this.redLock && this.blueLock && this.greenLock) {
			this.gate.transform.position = this.doorOpenPosition;
			this.gate.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, 0f, transform.eulerAngles.z);
		}
	}

	// Update the life text with the current lives
	void UpdateLifeLabel(){
		string lifeText = "Life:";
		for (int i = 0; i < this.lives; i++) {
			lifeText += " <3";
		}
		this.lifeLabel.text = lifeText;
	}

	// Game is over, hide the player and set info text
	void GameOver(){
		this.gameOver = true;
		for (int i = 0; i < playerRenderer.Length; i++) {
			playerRenderer[i].enabled = false;
			Instantiate(this.explosion, player.transform.position, Quaternion.identity);
			clips[0].Play ();
		}
		this.infoLabel.text = "Game Over\nPress Any Key to Restart";
	}

	// Clear the info text
	public void ClearInfoText(){
		this.infoLabel.text = "";
		this.displaying = false;
	}

	// Set the displaying to true
	public void Displaying(){
		this.displayTimer = 2f;
		this.displaying = true;
	}

	// Play the key pick up sound
	public void PlayKeySound(){
		this.clips [1].Play ();
	}
}
