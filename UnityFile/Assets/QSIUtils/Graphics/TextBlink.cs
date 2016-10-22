﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Text))]

public class TextBlink : MonoBehaviour 
{
	public Color col1;
	public Color col2;
	public float col1BlinkRate;
	public float col2BlinkRate;
    public float blinkTime = 3;
    
    public bool blinkForever = false;

	private Text sr;
	private bool blinkFlag;
    public float blinkClock;
    // Use this for initialization
    void Start () 
	{
		
        Reset();
    }

    public void Reset()
    {
        sr = GetComponent<Text>();
        Debug.Log("TextBlink: Reset");
        sr.color = col1;
        blinkClock = blinkTime;
    }
	
	// Update is called once per frame
	void Update () 
	{
        if(blinkClock > 0 || blinkForever)
        {
            if(blinkClock > 0)
                blinkClock -= Time.deltaTime;

            if (sr.color == col1 || sr.color == col2)
                blinkFlag = !blinkFlag;

            if (blinkFlag)
            {
                sr.color = Color.Lerp(sr.color, col1, col1BlinkRate);
            }
            else
            {
                sr.color = Color.Lerp(sr.color, col2, col2BlinkRate);
            }

        }
        else
        {
            sr.color = col1;
            enabled = false;
        }

    }
}
