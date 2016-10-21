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

    public Radar myRadar;

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
        UpdateTarget();
        HandleChase();
    }

    //keep searching for more appripriate targets
    void UpdateTarget()
    {
        if(myRadar != null)
        {
            //use the radar priorities to specify most desired targets
            for(int i = 0; i < myRadar.closestPerTag.Length; i++)
            {
                //i.e., only chase the subsequent priority type if there is none
                //of the higher priority type that is eligible.
                GameObject candidate = myRadar.closestPerTag[i];
                if (candidate != null)
                {
                    float range = Vector3.Distance(candidate.transform.position, transform.position);
                    if(range <= maxChaseRange)
                    { 
                        target = myRadar.closestPerTag[i].transform;
                        break;
                    }
                }
            }
        }
    }

    void HandleChase()
    {
        dist2Target = Dist2Target;

        //keep chasing target while it is in range
        if (InChaseRange)
        {
            mover.Destination = target;
            patrol.enabled = false;
        }
        else
        {
            //this means that the target is too far away, time to back off...
            if (dist2Target > minChaseRange)
            {
                patrol.enabled = true;
                patrol.SetDestination();
            }
            else
            {
                //this means you're right up against your target, don't move into
                //the target..
                patrol.enabled = false;
                mover.Destination = transform;
            }
        }
    }
}
