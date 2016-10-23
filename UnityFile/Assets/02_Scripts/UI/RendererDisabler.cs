using UnityEngine;
using System.Collections;

public class RendererDisabler : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        GetComponent<Renderer>().enabled = false;
	}

}
