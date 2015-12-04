/* Source File Name: EnemyAI.cs
 * Author's Name: Zhaoning Cai
 * Last Modified on: Dec 4, 2015
 * Program Description: Third Person Stealth Game. Player escapes the dungeon to win
 * the level. The player needs to solve puzzles and avoid/deactivate enemies
 * Revision History: Final Version
 */
using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

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
	}

	void Update(){

		// Game Logic, if kill timer becomes equal or less than 0, the enemy must be chasing
		// the player, in this case, shooting should be prioritized. If the enemy is chasing
		// the player, check to see if the chase timer becomes equal or less than 0, the
		// enemy should stop chasing the player and go back to patrolling, otherwise, the
		// enemy should continue chasing the player

		if (this.killTimer <= 0) {
			Shooting ();

		}
		if (chasing) {

			if (this.chaseTimer <= 0) {
			chasing = false;
			this.chaseTimer = this.chaseWait;
			Patrolling ();
			} else {
				Chasing ();
			}
		} else {
			Patrolling();
		}

	}

	// The enemy stops and summon a flame to attack the player
	void Shooting(){
		nav.Stop ();
		Instantiate (_particleSystem, playerObject.transform.position, Quaternion.identity);
		this._animator.SetInteger("AnimeState", 1);

	}

	// The enemy moves towards the player
	void Chasing(){
		nav.destination = player.transform.position;
		nav.speed = chaseSpeed;
		chaseTimer -= Time.deltaTime;

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

	// When the player stays within the detection range, kill timer start counting
	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			this.killTimer -= Time.deltaTime;
		}
	}

	// When the player enters the detection range, the enemy start chasing the player
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			this.clip.Play ();
			Chasing();
			this.chasing = true;
		}
	}

	// Upon finish playing the attack animation, an event at the end of the animation
	// calls this method to continue the enemy's patrolling.
	void ContinueAction(){
		nav.Resume ();

		this.chasing = false;

		this._animator.SetInteger("AnimeState", 0);
		this.killTimer = this.killWait;
		this.chaseTimer = this.chaseWait;
		gameController.ReduceLives ();
		Patrolling ();


	}

	
}
