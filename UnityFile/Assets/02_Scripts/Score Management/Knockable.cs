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
            float bonus = (rBody.velocity.magnitude * velocityMultiplier);
            float result = baseScore + bonus;
            Debug.Log("Score: " + result + "; bonus component :> " + bonus);
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
            ScoreManager.instance.AddScore(ActualScore,comboTimerBonus);
            knocked = true;

            SpecialMessage sm = GetComponent<SpecialMessage>();

            if (sm != null)
                sm.Trigger();
        }
    }
}
