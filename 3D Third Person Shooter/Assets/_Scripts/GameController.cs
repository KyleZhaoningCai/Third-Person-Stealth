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
	private bool hasAxe = false;
	private bool hasSleepingPill = false;
	private bool hasPoison = false;
	private bool hasWater = false;
	private bool hasEmptyBottle = false;
	private bool hasWine = false;
	private float cooldown = 0.5f;
	private bool offCooldown = true;
	private Transform chandelierTransform;
	private string currentStatus = "";
	private float gameOverCountDown = 2.5f;
	private bool delayGameOver = false;


	// Public References ++++++++++++++++++++++++++++++++
	public GameObject explosion;
	public GameObject gate;
	public Text lifeLabel;
	public Text infoLabel;
	public GameObject chandelierExplosion;
	public GameObject chandelierEnemyExplosion;
	public Transform wayPointHuman;
	public Transform wayPointTargetEnemy;

	// Public Read-Only
	public bool _GameOver{
		get {
			return this.gameOver;
		}
	}
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

	public bool _HasAxe {
		get {
			return this.hasAxe;
		}
	}
	
	public bool _HasSleepingPill {
		get {
			return this.hasSleepingPill;
		}
	}
	
	public bool _HasPoison {
		get {
			return this.hasPoison;
		}
	}
	
	public bool _HasWater {
		get {
			return this.hasWater;
		}
	}
	
	public bool _HasEmptyBottle{
		get {
			return this.hasEmptyBottle;
		}
	}
	
	public bool _HasWine {
		get {
			return this.hasWine;
		}
	}

	public bool _OffCooldown {
		get {
			return this.offCooldown;
		}
	}
	
	public string _CurrentStatus {
		get {
			return this.currentStatus;
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
		chandelierTransform = GameObject.FindGameObjectWithTag ("Chandelier").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		// If the game is over, allow player to press any key to restart
		if (gameOver) {
			if (this.offCooldown)
			{
				if (Input.anyKeyDown){
					Application.LoadLevel(Application.loadedLevel);
					gameOver = false;
				}
			}else{
				this.ActionCoolDown();
			}
		}
	
		// If the info text is displaying, delete the text after two seconds
		if (this.displaying) {
			this.displayTimer -= Time.deltaTime;
			if(this.displayTimer <= 0){
				ClearInfoText();
			}
		}

		if (delayGameOver) {
			this.gameOverCountDown -= Time.deltaTime;
			if (this.gameOverCountDown <= 0){
				this.GameOver("You Missed Your Chance to Escape");
				this.delayGameOver = false;
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
			this.GameOver("Game Over\nPress Any Key to Restart");
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
	public void GameOver(string text){
		this.gameOver = true;
		for (int i = 0; i < playerRenderer.Length; i++) {
			playerRenderer[i].enabled = false;
		}
		Instantiate(this.explosion, player.transform.position, Quaternion.identity);
		this.clips [0].Play ();
		this.displayTimer = 1000f;
		this.EnterCoolDown();
		this.infoLabel.text = text;
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

	public void ObtainEmptyBottle(){
		this.hasEmptyBottle = true;
	}
	public void BottleUpSleepingPill(){
		this.hasSleepingPill = true;
		this.hasPoison = false;
		this.hasWater = false;
	}
	public void BottleUpPoison(){
		this.hasSleepingPill = false;
		this.hasPoison = true;
		this.hasWater = false;
	}
	public void BottleUpWater(){
		this.hasSleepingPill = false;
		this.hasPoison = false;
		this.hasWater = true;
	}
	public void ActionCoolDown(){
		this.cooldown -= Time.deltaTime;
		if (this.cooldown <= 0){
			this.offCooldown = true;
			this.cooldown = 0.5f;
		}
	}
	public void EnterCoolDown(){
		this.offCooldown = false;
	}

	public void ObtainWine(){
		this.hasWine = true;
	}

	public void LoseWine(){
		this.hasWine = false;
	}

	public void PickUpAxe(){
		this.hasAxe = true;
	}
	public void ChandelierExplode(bool isEnemy){
		if (isEnemy) {
			Instantiate (this.chandelierEnemyExplosion, this.chandelierTransform.position, Quaternion.identity);
		} else {
			Instantiate (this.chandelierExplosion, this.chandelierTransform.position, Quaternion.identity);
		}
	}
	public void SetTarget(Vector3 position){
		this.wayPointHuman.position = position;
	}
	public void SetCurrentStatus(string status){
		this.currentStatus = status;
	}
	public void WakeUp(Vector3 position){
		this.wayPointTargetEnemy.position = position;
	}
	public void DelayGameOver(){
		this.delayGameOver = true;
	}
}
