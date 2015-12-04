using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class OpenJailDoor : MonoBehaviour {

	private Vector3 doorOpenPosition;
	private Transform _transform;

	public GameController gameController;

	// Use this for initialization
	void Start () {
		doorOpenPosition = new Vector3 (1.85f, 0f, -0.15f);
		this._transform = gameObject.GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if (other.CompareTag ("Player")) {
			if (CrossPlatformInputManager.GetButtonDown("Fire1")){
				if (this.gameController._HasKey){
					this._transform.position = this.doorOpenPosition;
				}
				else{
				}
			}
		}
	}

}
