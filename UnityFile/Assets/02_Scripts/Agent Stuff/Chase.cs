/*
NSI GameJam 11
Mad Dog

Navmesh Agent Stuff

-Matt Cabanag
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DestinationMover))]
[RequireComponent(typeof(Patrol))]
public class Chase : MonoBehaviour
{
    public Transform target;
    public float maxChaseRange = 2;
    public float minChaseRange = 0;

    public float Dist2Target
    {
        get
        {
            float result = -1;

            if (target != null)
                result = Vector3.Distance(target.position, transform.position);

            dist2Target = result;
            return result;
        }
    }

    public bool InChaseRange
    {
        get
        {
            dist2Target = Dist2Target;

            return dist2Target < maxChaseRange && dist2Target > minChaseRange;
        }
    }

    private Patrol patrol;
    private DestinationMover mover;
    private float dist2Target;

    // Use this for initialization
    void Start ()
    {
        patrol = GetComponent<Patrol>();
        mover = GetComponent<DestinationMover>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleChase();
    }

    void HandleChase()
    {
        dist2Target = Dist2Target;

        if (InChaseRange)
        {
            mover.Destination = target;
            patrol.enabled = false;
        }
        else
        {
            patrol.enabled = true;
            patrol.SetDestination();
        }
    }
}
