/* Source File Name: PickUpWine.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;

public class PickUpWine : MonoBehaviour {
	
	//Public Instance Variables +++++++++++++++++++++++++++++++++++++
	
	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float chaseWait = 5f;
	public Transform[] patrolWayPoints;
	public float chaseTimer = 5f;
	public float killTimer = 3f;
	public float killWait = 3f;
	public GameObject _particleSystem;
	public GameController gameController;
	
	//Private Instance Variables ++++++++++++++++++++++++++++++++++++
	private Animator _animator;
	private GameObject playerObject;
	private bool chasing;
	private NavMeshAgent nav;
	private Transform player;
	private float chaseTime;
	private int wayPointIndex;
	private Collider smallCollider;
	private Collider[] colliders;
	private AudioSource clip;
	private bool attacking = false;
	private float attackTimer = 3f;
	private float attackWait = 3f;
	private float drinkDelay = 4f;
	private bool gotWine = false;
	private string status = "";
	private bool asleep = false;
	private Vector3 originalWayPointHuman;
	private Vector3 originalWayPointExplosionEnemy;
	private float sleepDuration = 7f;
	
	void Awake(){
		// References and Set Default Values;
		_animator = gameObject.GetComponent<Animator> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		nav = gameObject.GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		colliders = gameObject.GetComponentsInChildren<Collider> ();
		colliders [1].enabled = false;
		chasing = false;
		clip = gameObject.GetComponent<AudioSource> ();
		originalWayPointHuman = new Vector3 (12.18f, 0.15f, -7.1f);
		originalWayPointExplosionEnemy = new Vector3 (12.18f, 0.15f, 5.82f);
	}
	
	void Update(){
		
		// Game Logic, if kill timer becomes equal or less than 0, the enemy must be chasing
		// the player, in this case, shooting should be prioritized. If the enemy is chasing
		// the player, check to see if the chase timer becomes equal or less than 0, the
		// enemy should stop chasing the player and go back to patrolling, otherwise, the
		// enemy should continue chasing the player

		if (this.asleep) {
			this.sleepDuration -= Time.deltaTime;
			if (this.sleepDuration <= 0){
				this.asleep = false;
				this.gameController.infoLabel.text = "Enemy Was Waken Up";
				this.gameController.WakeUp(originalWayPointExplosionEnemy);
				this.sleepDuration = 7f;
				this.ContinueAction();
			}
		}

		if (this.gotWine) {
			drinkDelay -= Time.deltaTime;
			if (drinkDelay <= 0) {


				if (this.status == "And Became Dizzy") {
					this.gameController.infoLabel.text = "The Enemy Drank the Wine\n" + this.status;
					this.gameController.Displaying ();
					this.gotWine = false;
					this.drinkDelay = 4f;
					this._animator.SetInteger ("AnimeState", 3);
					this.asleep = true;
					this.gameController.WakeUp (this.transform.position);
					this.nav.Stop ();
				} else if (this.status == "And Was Paralyzed Permanently") {
					this.gameController.infoLabel.text = "The Enemy Drank the Wine\n" + this.status;
					this.gameController.Displaying ();
					for (int i = 0; i < this.colliders.Length; i++) {
						this.colliders [i].enabled = false;
						this.nav.Stop ();
						this._animator.SetInteger ("AnimeState", 2);
						this.gameController.DelayGameOver();
						this.gotWine = false;
					}
				}else if (this.status == "But Nothing Happened"){
					this.drinkDelay = 4f;
					this.gotWine = false;
					this.gameController.infoLabel.text = "The Enemy Drank the Wine\n" + this.status;
					this.gameController.Displaying ();

				}
			}else{
				Patrolling();
			}
		} else {
			Patrolling();
		}
		
	}

	
	// The enemy patrols on a set path, moving from waypoint to waypoint
	void Patrolling(){
		nav.speed = patrolSpeed;
		
		if (nav.remainingDistance < nav.stoppingDistance) {
			if (wayPointIndex == patrolWayPoints.Length - 1){
				wayPointIndex = 0;
			}else{
				wayPointIndex++;
			}
		}
		nav.destination = patrolWayPoints [wayPointIndex].position;
	}

	
	// When the player enters the detection range, the enemy start chasing the player
	void OnTriggerEnter(Collider other){

		if (other.CompareTag("FishyWine")){
			Destroy (other.gameObject);
			this.gotWine = true;
			this.status = this.gameController._CurrentStatus;
			this.gameController.SetTarget(this.originalWayPointHuman);
		}

	}
	
	// Upon finish playing the attack animation, an event at the end of the animation
	// calls this method to continue the enemy's patrolling.
	void ContinueAction(){
		nav.Resume ();

		
		this._animator.SetInteger("AnimeState", 0);

		Patrolling ();
		
		
	}
	
	
	
	
}
