using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float chaseWait = 5f;
	public Transform[] patrolWayPoints;
	public float chaseTimer = 5f;
	public float killTimer = 3f;
	public float killWait = 3f;
	public GameObject particleSystem;
	public GameController gameController;

	private Animator _animator;
	private GameObject playerObject;
	private bool chasing;
	private NavMeshAgent nav;
	private Transform player;
	private float chaseTime;
	private int wayPointIndex;

	void Awake(){
		_animator = gameObject.GetComponent<Animator> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		nav = gameObject.GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		chasing = false;
	}

	void Update(){
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

	void Shooting(){
		nav.Stop ();
		this._animator.SetInteger("AnimeState", 1);
		Instantiate (particleSystem, playerObject.transform.position, Quaternion.identity);
		gameController.ReduceLives ();
//		Destroy (playerObject.gameObject);

	}

	void Chasing(){
		nav.destination = player.transform.position;
		nav.speed = chaseSpeed;
		chaseTimer -= Time.deltaTime;

	}

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

	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			this.killTimer -= Time.deltaTime;
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			Chasing();
			this.chasing = true;
		}
	}
	void ContinueAction(){
		nav.Resume ();

		this.chasing = false;

		this._animator.SetInteger("AnimeState", 0);
		Patrolling ();

		this.killTimer = this.killWait;
		this.chaseTimer = this.chaseWait;
	}

	
}
