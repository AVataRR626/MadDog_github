/*
NSI GameJam 11
Mad Dog

The knockable things...
-Matt Cabanag
*/
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Knockable : MonoBehaviour
{
    public float baseScore = 1;
    public float velocityMultiplier = 1.5f;
    public GameObject scoreFX;
    public float comboTimerBonus = 0.5f;

    public bool knocked = false;

    public int ActualScore
    {
        get
        {
            float result = baseScore + (rBody.velocity.magnitude * velocityMultiplier);
            return (int)result;
        }
    }

    Rigidbody rBody;

	// Use this for initialization
	void Start ()
    {
        rBody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {


    }

    void OnCollisionEnter(Collision c)
    {
        if(!knocked && ScoreManager.instance.Armed)
        { 
            ScoreManager.instance.AddScore(ActualScore);
            knocked = true;

            SpecialMessage sm = GetComponent<SpecialMessage>();

            if (sm != null)
                sm.Trigger();
        }
    }
}
