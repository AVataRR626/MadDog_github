﻿using UnityEngine;
using System.Collections;

public class Poopable : MonoBehaviour
{
    public float baseScore = 1;
    public string poopTag = "Poop";

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("POOP: " + col.gameObject.tag + "," + poopTag);

        if(col.gameObject.tag.Contains(poopTag))
        {
            ScoreManager.instance.AddScore((int)baseScore);
        }
    }
}
