using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{


	public float rotSpeed = 30f;

    private float originalY;

	// Use this for initialization
	void Start () {
        originalY = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0, rotSpeed * Time.deltaTime, 0);
		transform.position = new Vector3(transform.position.x, originalY + Mathf.PingPong(Mathf.Sin (Time.time),1f), transform.position.z);
	}

	public void OnTriggerEnter(Collider other){
		if (other.GetComponent<PlayerMovement> () != null) {
			other.GetComponent<PlayerMovement> ().AddAmmo ();
			Destroy (this.gameObject);
		}
	}
}
