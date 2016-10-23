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
            return isInRange;
        }
    }

    public bool CloseChaseRange
    {
        get
        {
            return dist2Target < minChaseRange;
        }
    }

    private Patrol patrol;
    private DestinationMover mover;
    private float dist2Target;
    private bool lineOfSight = false;
    private bool isInRange = false;

    // Use this for initialization
    void Start ()
    {
        patrol = GetComponent<Patrol>();
        mover = GetComponent<DestinationMover>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckLineOfSight();

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
                        if(CheckLineOfSight(candidate, transform.position))
                        { 
                            target = candidate.transform;
                            break;
                        }
                    }
                }
            }
        }
    }

    void HandleChase()
    {
        dist2Target = Dist2Target;

        isInRange = false;
        lineOfSight = false;

        Debug.Log("AAAA");

        if (target != null)
        {  
            //if in chase range.... go towards the target
            if(dist2Target > minChaseRange && dist2Target < maxChaseRange)
            {
                isInRange = true;
                //check if line of sight is clear
                lineOfSight = CheckLineOfSight(target.gameObject, transform.position);
                if (lineOfSight)
                {
                    //distance and line of sight tests passed, go chase!
                    mover.Destination = target;
                    patrol.enabled = false;
                }
                else
                {
                    //resume previous tasks...
                    target = null;
                    ResumePatrol();
                }
            }
            else if(dist2Target > maxChaseRange)
            {
                //target too far away, resume previous tasks...
                target = null;
                mover.Destination = transform;
                ResumePatrol();
            }
            else if(dist2Target < minChaseRange)
            {
                //you've reached your target, don't chase any further
                mover.Destination = transform;
                patrol.enabled = false;
                isInRange = true;
            }
        }
        else
        {
            Debug.Log("ZZZ");
            target = null;
            ResumePatrol();
        }
    }

    public void ResumePatrol()
    {
        if(mover.Destination == null)
            mover.Destination = transform;

        patrol.enabled = true;
    }

    void CheckLineOfSight()
    {
        if(target != null)
        {
            lineOfSight = CheckLineOfSight(target.gameObject, transform.position);

            if (!lineOfSight)
                target = null;
        }
    }

    //checks if there is a straight line of sight from origin to target object
    //returns true if no obstructions
    //returns false if there is.
    public static bool CheckLineOfSight(GameObject target, Vector3 origin)
    {
        bool result = false;

        Vector3 toTarget = target.transform.position - origin;

        Ray rayToTarget = new Ray(origin, toTarget);

        RaycastHit hit;

        if (Physics.Raycast(rayToTarget, out hit, 100))
        {
            Debug.DrawLine(rayToTarget.origin, hit.point);


            //first hit is the target, no obstructions!
            if (hit.collider.gameObject == target)
                result = true;                
        }


        //Debug.Log("Raycast hit a " + hit.collider.name + " result:" + result);

        return result;
    }
}
