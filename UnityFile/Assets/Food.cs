using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
	public float rotSpeed = 30f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, rotSpeed * Time.deltaTime, 0);
		transform.position = new Vector3(transform.position.x, Mathf.PingPong(Mathf.Sin (Time.time),1f)-1, transform.position.z);
	}

	public void OnTriggerEnter(Collider other){
		if (other.GetComponent<PlayerMovement> () != null) {
			other.GetComponent<PlayerMovement> ().AddAmmo ();
			Destroy (this.gameObject);
		}
	}
}
