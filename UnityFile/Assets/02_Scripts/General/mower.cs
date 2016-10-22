using UnityEngine;
using System.Collections;


[RequireComponent(typeof(PlayerMovement))]
public class mower : MonoBehaviour {


	public Transform spawnLoc;
	public GameObject Particle;

	private PlayerMovement M;
	private PlayerMovement dog;

	private FixedJoint joint;
	private float ActivateTime;
	// Use this for initialization
	void Start () {
		M = GetComponent<PlayerMovement>();
	}
	
	// TODO: add score hook in.
	void Update () {
		if(M.getActive() && Input.GetButtonDown("Jump") && Time.time - ActivateTime > 1f){//go back to dog.
			dog.setActive();
			Destroy(GetComponent<FixedJoint>());
			M.setActive();
		}
		if(M.getActive()){
			Particle.SetActive(true);
		}else{
			Particle.SetActive(false);
		}
	}


	 void OnTriggerEnter(Collider other) {
		if(other.GetComponent<PlayerMovement>() != null){//switch to mower
			dog = other.GetComponent<PlayerMovement>();
			dog.setActive();
			M.setActive();
			ActivateTime = Time.time;
			Rigidbody rb = other.GetComponent<Rigidbody>();
			joint = this.gameObject.AddComponent<FixedJoint>();
			rb.velocity = Vector3.zero;
			
			other.transform.position = spawnLoc.position;
			other.transform.rotation = spawnLoc.rotation;
			joint.connectedBody = rb;
			other.transform.parent = transform;

		}
	}
}
