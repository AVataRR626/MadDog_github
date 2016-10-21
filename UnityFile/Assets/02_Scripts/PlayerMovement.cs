using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour {
	public float poopAnimTime = 1f;

	[SerializeField]
	private float rotSpeed = 100f;
	[SerializeField]
	private float moveSpeed = 5f;
	[SerializeField]
	private float jumpPower = 5f;

	private bool freeze = false;
	private float lastTime = -5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("poop")) {
			Freeze();
		}
		
		float rVector =0f,mVector=0f;
		if (Input.GetButton("D")) rVector +=1;
		if (Input.GetButton("A")) rVector -=1;
		if (Input.GetButton("W")) mVector +=1;
		if (Input.GetButton("S")) mVector -=1;
		
		
		transform.Rotate(new Vector3(0,rVector*rotSpeed*Time.deltaTime,0));

		if((!Physics.Raycast(transform.position, transform.forward,0.5f) || mVector<0 ) && !freeze)
			transform.Translate((transform.forward) * moveSpeed * Time.deltaTime * mVector,Space.World);
		if ((Input.GetButtonDown("Jump") && Time.time - lastTime > 1f) && !freeze){
			GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
			lastTime = Time.time;
		}
	}

	public void Freeze(){
		Debug.Log("Freeze");
		freeze = true;
		StartCoroutine("freezeTime");
	}

	IEnumerator freezeTime(){
		yield return new WaitForSeconds(poopAnimTime);
		Debug.Log("unFreeze");
		freeze = false;
	}
}
