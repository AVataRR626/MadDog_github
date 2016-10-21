using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour {
	//TODO: set this
	private float poopAnimTime = 1f;

	public bool jumpable = true;

	//if this is the current active movement script
	//for use with ride-on etc - only one should be acttive
	[SerializeField]
	private bool isActiveMovement;

	//movement vars
	private float rotSpeed = 150f;
	private float moveSpeed = 8f;
	private float jumpPower = 300f;
	//for animations
	private bool freeze = false;
	private float lastTime = -5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	if(isActiveMovement){
		if(Input.GetButtonDown("poop")) {
			Freeze();
		}
		
		float rVector =0f,mVector=0f;
		if (Input.GetButton("S")) {//reverses left right while moving backwards
			mVector -=1;
			if (Input.GetButton("D")) rVector -=1;
			if (Input.GetButton("A")) rVector +=1;
		}else{
			if (Input.GetButton("D")) rVector +=1;
			if (Input.GetButton("A")) rVector -=1;
		}
		if (Input.GetButton("W")) mVector +=1;
		
		
		transform.Rotate(new Vector3(0, rVector * rotSpeed * Time.deltaTime,0));

		Ray ray = new Ray(transform.position,transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.5f)) {
			if (!hit.transform.gameObject.isStatic || mVector < 0)// if not near wall or backing up
				move(mVector);
		}else
			move(mVector);//normal movement


		if ((Input.GetButtonDown("Jump") && Time.time - lastTime > 1f) && !freeze){
			if(Physics.Raycast(transform.position,transform.up * -1 , 2f)){
				GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower);
				lastTime = Time.time;
			}
		}
		}
	}


	private void move(float mVector){
	if(!freeze)
		transform.Translate((transform.forward) * moveSpeed * Time.deltaTime * mVector,Space.World);
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

	public void setActive(){
		isActiveMovement = ! isActiveMovement;
	}

	public bool getActive() {return isActiveMovement;}
}
