/*
NSI GameJam 11
Mad Dog

Navmesh Agent Stuff

-Matt Cabanag
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class DestinationMover : MonoBehaviour
{
    public Transform destination;
    public NavMeshAgent agent;

    public Transform Destination
    {
        get
        {
            return destination;
        }

        set
        {
            destination = value;
            agent.destination = destination.position;
        }
    }
	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(destination != null)
            agent.destination = destination.position;

    }
}
