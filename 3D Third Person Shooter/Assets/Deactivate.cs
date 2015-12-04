using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class Deactivate : MonoBehaviour {

	private GameObject[] enemies;
	private SphereCollider sphereCollider;

	public Animator _animator;
	public NavMeshAgent navMeshAgent;

	void Start(){
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		sphereCollider = enemies [0].GetComponent<SphereCollider> ();
	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag("Player")){
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				sphereCollider.enabled = false;
				navMeshAgent.Stop();
				this._animator.SetInteger("AnimeState", 2);

			}
		}
	}
}
