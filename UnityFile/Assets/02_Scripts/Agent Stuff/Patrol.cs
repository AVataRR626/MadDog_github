/*
NSI GameJam 11
Mad Dog

Navmesh Agent Stuff

-Matt Cabanag
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DestinationMover))]
public class Patrol : MonoBehaviour
{
    public int patrolIndex = 0;
    public Transform [] patrolPoints;

    private DestinationMover mover;
    // Use this for initialization
    void Start ()
    {
        mover = GetComponent<DestinationMover>();
        SetDestination();
    }
	
	// Update is called once per frame
	void Update ()
    {

        CyclePatrolPoints();
    }

    public void CyclePatrolPoints()
    {
        if (patrolPoints.Length > 0)
        {
            if (mover.agent.remainingDistance <= mover.agent.stoppingDistance)
            {
                Debug.Log("Moving to next patrol point!");
                patrolIndex++;
                patrolIndex = LoopInt(patrolIndex, 0, patrolPoints.Length - 1);
                SetDestination();
            }
        }
    }

    public void SetDestination(int i)
    {
        if (patrolPoints.Length > 0)
        {
            patrolIndex = i;
            SetDestination();
        }
    }

    public void SetDestination()
    {
        if (patrolPoints.Length > 0)
            mover.Destination = patrolPoints[patrolIndex];
    }

    public static int LoopInt(int value, int min, int max)
    {
        if (value > max)
            value = min;

        if (value < min)
            value = max;

        return value;
    }
}
