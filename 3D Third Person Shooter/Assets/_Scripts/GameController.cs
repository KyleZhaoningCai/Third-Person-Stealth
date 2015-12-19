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
	// Level 1
	private int lives = 3;
	private bool hasKey;
	private bool hasCode1;
	private bool hasCode2;
	private bool hasCode3;
	private bool redLock;
	private bool blueLock;
	private bool greenLock;
	private bool stopped1 = false;
	private bool stopped2 = false;
	private bool stopped3 = false;
	private Vector3 doorOpenPosition;
	private bool gameOver;
	private GameObject player;
	private Renderer[] playerRenderer;
	private float displayTimer = 2f;
	private bool displaying = false;
	private AudioSource[] clips;
	// Level 2
	private bool hasAxe = false;
	private bool hasSleepingPill = false;
	private bool hasPoison = false;
	private bool hasWater = false;
	private bool hasEmptyBottle = false;
	private bool hasWine = false;
	private float cooldown = 0.5f;
	private bool offCooldown = true;
	private Transform chandelierTransform;
	// Level 3
	private string currentStatus = "";
	private float gameOverCountDown = 2.5f;
	private bool delayGameOver = false;
	private bool move1 = false;
	private bool move2 = false;
	private bool move3 = false;
	private bool move4 = false;
	private bool move5 = false;
	private bool N1 = false;
	private bool N2 = false;
	private bool NW1 = false;
	private bool NW2 = false;
	private bool E = false;
	private bool S = false;
	private bool SE = false;
	private GameObject[] bomb;
	private int shouldExplode = 0;
	private float explosionTimer = 180f;
	private bool timerStarted = false;
	private GameObject bombButtons;
	private bool enabling = false;
	private bool redPressed = false;
	private bool greenPressed = false;
	private bool bluePressed = false;
	private bool purplePressed = false;
	private GameObject[] mainGate;
	private float endGameTimer = 3f;
	private bool endGame = false;

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

	public bool _Stopped1 {
		get {
			return this.stopped1;
		}
	}
	public bool _Stopped2 {
		get {
			return this.stopped2;
		}
	}
	public bool _Stopped3 {
		get {
			return this.stopped3;
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

	public bool _Move1 {
		get {
			return this.move1;
		}
	}

	public bool _Move2 {
		get {
			return this.move2;
		}
	}

	public bool _Move3 {
		get {
			return this.move3;
		}
	}
	public bool _Move4 {
		get {
			return this.move4;
		}
	}
	public bool _Move5 {
		get {
			return this.move5;
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
		try{
			chandelierTransform = GameObject.FindGameObjectWithTag ("Chandelier").GetComponent<Transform> ();
			bomb = GameObject.FindGameObjectsWithTag ("Bomb");
			this.bombButtons = GameObject.FindGameObjectWithTag ("BombButtons");
			mainGate = GameObject.FindGameObjectsWithTag ("MainGate");
		}catch{

		}

	}

	// Update is called once per frame
	void Update () {
		// If the game is over, allow player to press any key to restart
		if (gameOver) {
			if (this.offCooldown)
			{
				if (Input.anyKeyDown){
					Application.LoadLevel(Application.loadedLevel);
					this.gameOver = false;
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

		// There will be a delay before the game is over
		if (delayGameOver) {
			this.gameOverCountDown -= Time.deltaTime;
			if (this.gameOverCountDown <= 0){
				this.GameOver("You Missed Your Chance to Escape\nPress Any Key to Restart");
				this.delayGameOver = false;
			}
		}
		// This is the game logic for tic tac toe
		if (this.shouldExplode == 0) {
			if (this.E && ((this.N1 && this.N2) == false) && ((this.NW1 && this.NW2) == false) && this.SE == false && this.S == false) {
				this.move2 = true;
			} else if (this.E == false && ((this.N1 && this.N2) || (this.NW1 && this.NW2) || this.SE || this.S)) {
				this.move1 = true;
				this.explosionTimer = 7f;
				this.shouldExplode = 2;
			} else if (this.E && this.move2 && (((this.N1 && this.N2) || this.S) && ((this.NW1 && this.NW2) == false))) {
				this.move4 = true;
				this.explosionTimer = 7f;
				this.shouldExplode = 2;
			} else if (this.E && this.move2 && (this.NW1 && this.NW2) && (this.N1 && this.N2) == false && this.S == false) {
				this.move3 = true;
			} else if (this.E && this.move2 && this.move4 && (this.N1 && this.N2) == false && this.S) {
				this.move5 = true;
			}
		}
		// Trigger bomb explode when exploding factor is 1
		if (this.shouldExplode == 1) {
			this.BombExplode();
		}
		// Start the bomb timer when player leaves the starting area on level 3
		if (this.timerStarted) {
			this.lifeLabel.text = (int)this.explosionTimer + " Seconds";
			this.explosionTimer -= Time.deltaTime;
			if (this.explosionTimer <= 0){
				this.shouldExplode = 1;
				this.timerStarted = false;
			}
		}
		// When computer makes a winning move, enable the explosion to kill the player
		if ((this.NW1 && this.NW2) && (this.N1 && this.N2)) {
			this.EnableBombButtons();
		}
		// When the player passes level 3, show winning scene after few seconds
		if (this.endGame) {
			this.endGameTimer -= Time.deltaTime;
			if (this.endGameTimer <= 0) {
				Application.LoadLevel (3);
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
	// Play the explosion sound
	public void PlayExplosionSound(){
		this.clips [0].Play ();
	}
	// Set has empty bottle to true
	public void ObtainEmptyBottle(){
		this.hasEmptyBottle = true;
	}
	// Player gains sleeping liquid and loses other liquid
	public void BottleUpSleepingPill(){
		this.hasSleepingPill = true;
		this.hasPoison = false;
		this.hasWater = false;
	}
	// Player gains poison liquid and loses other liquid
	public void BottleUpPoison(){
		this.hasSleepingPill = false;
		this.hasPoison = true;
		this.hasWater = false;
	}
	// Player gains water and loses other liquid
	public void BottleUpWater(){
		this.hasSleepingPill = false;
		this.hasPoison = false;
		this.hasWater = true;
	}
	// Cool down timer which does not allow player to perform most other moves before this timer hits 0
	public void ActionCoolDown(){
		this.cooldown -= Time.deltaTime;
		if (this.cooldown <= 0){
			this.offCooldown = true;
			this.cooldown = 0.5f;
		}
	}
	// Enter action cool down
	public void EnterCoolDown(){
		this.offCooldown = false;
	}

	public void ObtainWine(){
		this.hasWine = true;
	}
	// Player loses the wine
	public void LoseWine(){
		this.hasWine = false;
	}
	// Player picks up axe
	public void PickUpAxe(){
		this.hasAxe = true;
	}
	// Chancelier does a small explosion  when hit non robot enemy, otherwise it does a big explosion to destroy the gate
	public void ChandelierExplode(bool isEnemy){
		if (isEnemy) {
			Instantiate (this.chandelierEnemyExplosion, this.chandelierTransform.position, Quaternion.identity);
		} else {
			Instantiate (this.chandelierExplosion, this.chandelierTransform.position, Quaternion.identity);
		}
	}
	// Set a new target for the human enemy
	public void SetTarget(Vector3 position){
		this.wayPointHuman.position = position;
	}
	// Set the current status of the human enemy after drinking wine
	public void SetCurrentStatus(string status){
		this.currentStatus = status;
	}
	// Makes the robot enemy to wake up the enemy
	public void WakeUp(Vector3 position){
		this.wayPointTargetEnemy.position = position;
	}
	// Set delay game over to true
	public void DelayGameOver(){
		this.delayGameOver = true;
	}
	// These two check the top left corner
	public void NW1Check(){
		this.NW1 = true;
	}
	public void NW2Check(){
		this.NW2 = true;
	}
	// THese two check the top grid
	public void N1Check(){
		this.N1 = true;
	}
	public void N2Check(){
		this.N2 = true;
	}
	// This checks the right grid
	public void ECheck(){
		this.E = true;
	}
	// This checks the bottom grid
	public void SCheck(){
		this.S = true;
	}
	// This checks the bottom right corner
	public void SECheck(){
		this.SE = true;
	}
	// Makes the bomb do a huge explosion and trigger game over
	void BombExplode(){
		Instantiate (this.chandelierEnemyExplosion, this.bomb[0].transform.position, this.bomb[0].transform.rotation);
		Destroy (this.bomb[1].gameObject);
		this.clips [0].Play ();
		this.GameOver ("Bomb Exploded and Killed You\nPress Any Key to Restart");
		this.shouldExplode = 2;
	}
	// Start the bomb timer
	public void StartTimer(){
		this.lifeLabel.text =  (int)this.explosionTimer + " Seconds";
		this.timerStarted = true;
	}
	// Make the bomb deactivation buttons appear
	void EnableBombButtons(){
		this.bombButtons.transform.position = new Vector3 (0, 0, 0);
		this.N1 = false;
	}
	// Player presses the four buttons
	public void PressRedButton(){
		this.redPressed = true;
		this.infoLabel.text = "You Pressed Red Button";
		this.Displaying();
	}
	public void PressGreenButton(){
		if (this.redPressed) {
			this.greenPressed = true;
			this.infoLabel.text = "You Pressed Green Button";
			this.Displaying();
		} else {
			this.explosionTimer = 7;
			this.bombButtons.transform.position = new Vector3 (0, -5, 0);
			this.infoLabel.text = "What a Mistake";
			this.Displaying();
		}
	}
	public void PressBlueButton(){
		if (this.greenPressed) {
			this.bluePressed = true;
			this.greenPressed = true;
			this.infoLabel.text = "You Pressed Blue Button";
		} else {
			this.explosionTimer = 7;
			this.bombButtons.transform.position = new Vector3 (0, -5, 0);
			this.bombButtons.transform.position = new Vector3 (0, -5, 0);
			this.infoLabel.text = "What a Mistake";
		}
	}
	public void PressPurpleButton(){
		if (this.bluePressed) {
			this.SmallExplode();
			this.explosionTimer = 180f;
			this.bombButtons.transform.position = new Vector3 (0, -5, 0);
		} else {
			this.explosionTimer = 7;
			this.bombButtons.transform.position = new Vector3 (0, -5, 0);
			this.bombButtons.transform.position = new Vector3 (0, -5, 0);
			this.infoLabel.text = "What a Mistake";
		}
	}
	// Makes the bomb do a small explosion which only destroy the gate
	public void SmallExplode(){
		Instantiate (this.chandelierExplosion, this.bomb[0].transform.position, this.bomb[0].transform.rotation);
		this.clips [0].Play ();
		Destroy (this.bomb[1].gameObject);
		for (int i = 0; i < this.mainGate.Length; i++) {
			Destroy (this.mainGate[i].gameObject);
		}
		this.timerStarted = false;
		this.shouldExplode = 2;
		this.endGame = true;

	}
	// Play a cutting sound
	public void PlayCutSound(){
		this.clips [5].Play ();
	}
	// Play a deactivation sound
	public void DeactivateSound(){
		this.clips [6].Play ();
	}



}
