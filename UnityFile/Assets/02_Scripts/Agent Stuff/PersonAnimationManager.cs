/*
NSI GameJam 11
Mad Dog

Person animation manager
-Matt Cabanag
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class PersonAnimationManager : MonoBehaviour
{
    [Header("Anim Parameters")]
    public float walkSpeedThreshold = 0.5f;
    public string runningStateString = "Running";
    public string actionStateString = "Cleaning";

    [Header("Component Links")]
    public Chase chaser;
    public DestinationMover mover;
    public Patrol patroller;

    Animator anim;

	// Use this for initialization
	void Start ()
    {
        if(chaser == null)
            chaser = transform.root.GetComponent<Chase>();

        if(mover == null)
            mover = transform.root.GetComponent<DestinationMover>();

        if (patroller == null)
            patroller = transform.root.GetComponent<Patrol>();


        anim = GetComponent<Animator>();
        anim.SetBool(runningStateString, false);
        anim.SetBool(actionStateString, false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        ManageAnimations();
    }

    void ManageAnimations()
    {
        if (mover.agent.velocity.magnitude > walkSpeedThreshold)
        {
            anim.SetBool(runningStateString, true);
            anim.SetBool(actionStateString, false);
        }
        else
        {
            anim.SetBool(runningStateString, false);
            anim.SetBool(actionStateString, false);

            if (chaser.target != null)
            {
                
                if(chaser.CloseChaseRange)
                {
                    anim.SetBool(actionStateString, true);
                }
                
            }
        }
    }
}
