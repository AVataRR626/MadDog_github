using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other){
		if (other.GetComponent<PlayerMovement> () != null) {
			other.GetComponent<PlayerMovement> ().AddAmmo ();
			Destroy (this.gameObject);
		}
	}
}
