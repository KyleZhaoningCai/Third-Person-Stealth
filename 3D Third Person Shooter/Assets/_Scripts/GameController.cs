using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int lives = 2;
	private bool hasKey;

	public bool _HasKey {
		get {
			return this.hasKey;
		}
	}

	// Use this for initialization
	void Start () {
		hasKey = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GotKey(){
		this.hasKey = true;
	}

	public void ReduceLives(){
		lives -= 1;
	}
}
