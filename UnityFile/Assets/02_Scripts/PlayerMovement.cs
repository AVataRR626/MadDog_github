using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour {
	//TODO: set this
	private float poopAnimTime = 1f;

	public bool jumpable = true;
	public GameObject poop;
	public Transform poopSpawnLoc;

	[SerializeField]
	private int poopAmmo = 5;

	//if this is the current active movement script
	//for use with ride-on etc - only one should be acttive
	[SerializeField]
	private bool isActiveMovement;

	//movement vars
	private float rotSpeed = 150f;
	private float moveSpeed = 8f;
	private float jumpPower = 300f;

	private bool sprinting = false;
	//for animations
	private bool freeze = false;
	private float lastTime = -5f;

	private Animator anim;
	// Use this for initialization
	void Start () {
		if(jumpable) anim = transform.GetChild(1).GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isActiveMovement)
			movement();
		else if (jumpable) 
			anim.SetBool("Running",false);

		if(jumpable)
			setAnim();

		if(Input.GetButtonDown("poop") && jumpable && poopAmmo > 4) {
			Freeze();
			Poop();
		}
	}
	
	private bool isGrounded(){
		return Physics.Raycast(transform.position,transform.up * -1 , 2f);
	}

	private void setAnim(){
		anim.SetBool("Sprinting",sprinting);
		anim.SetBool("Jumping",!isGrounded());
	}

	private void Poop(){
		//pooAmmo = 0;
		Debug.Log("Pooping");
		Instantiate(poop,poopSpawnLoc.position,Quaternion.identity);
	}

	private void movement(){
		

		if(Input.GetButton("Sprint")) sprinting = true;
		else sprinting = false;
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

	private void move(float mVector){
		if(!freeze){
			transform.Translate((transform.forward) * moveSpeed * ((System.Convert.ToInt32(sprinting))+1) * Time.deltaTime * mVector,Space.World);
			if(jumpable){
				anim.SetBool("Running",true);
				if(mVector == 0) anim.SetBool("Running",false);
			}
				
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

	public void setActive(){
		isActiveMovement = ! isActiveMovement;
	}

	public bool getActive() {return isActiveMovement;}
}
