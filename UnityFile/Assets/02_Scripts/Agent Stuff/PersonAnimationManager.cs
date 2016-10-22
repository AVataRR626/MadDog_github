using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class PersonAnimationManager : MonoBehaviour
{

    Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
