using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	
	[SerializeField]
	private float force;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void OnTriggerEnter(Collider other){
		if (other.transform.gameObject.tag == "Player"){
			other.GetComponent<Rigidbody>().AddForce(Vector3.up*force);
		}
	}
}
