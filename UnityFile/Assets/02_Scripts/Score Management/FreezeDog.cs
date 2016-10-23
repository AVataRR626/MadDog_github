using UnityEngine;
using System.Collections;

public class FreezeDog : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().Speed = 0;
    }

}
