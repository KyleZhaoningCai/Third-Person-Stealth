using UnityEngine;
using System.Collections;

public class LazerKill : MonoBehaviour {

	public GameController gameController;

	private GameObject humanEnemy;
	private NavMeshAgent nav;
	private EnemyAI enemyAI;

	// Use this for initialization
	void Start () {
		humanEnemy = GameObject.FindGameObjectWithTag ("HumanEnemy");
		nav = humanEnemy.GetComponent<NavMeshAgent> ();
		enemyAI = GameObject.FindGameObjectWithTag ("HumanEnemy").GetComponent<EnemyAI> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
			this.gameController.GameOver("Killed by Lazer\nPress Any Key to Continue");
		}
		if (other.CompareTag ("FishyWine")) {
			if (this.gameController._HasSleepingPill) {
				this.gameController.SetCurrentStatus ("And Became Dizzy");
			} else if (this.gameController._HasPoison) {
				this.gameController.SetCurrentStatus ("And Was Paralyzed Permanently");
			} else if (this.gameController._HasWater) {
				this.gameController.SetCurrentStatus ("But Nothing Happened");
			}
		}
	}
	void OnTriggerStay(Collider other){

		if (other.CompareTag("FishyWine")){
			this.gameController.SetTarget(other.transform.position);
		}
	}
}
