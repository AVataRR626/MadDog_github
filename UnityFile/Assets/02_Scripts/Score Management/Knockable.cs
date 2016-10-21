using UnityEngine;
using System.Collections;

public class Knockable : MonoBehaviour
{
    public float baseScore = 1;
    public float velocityMultiplier = 1.5f;
    public GameObject scoreFX;
    public float comboTimerBonus = 0.5f;

    public bool knocked = false;

	// Use this for initialization
	void Start ()
    {
	    
	}

    // Update is called once per frame
    void Update()
    {


    }

    void OnCollisionEnter(Collision c)
    {
        if(!knocked && ScoreManager.instance.Armed)
        { 
            ScoreManager.instance.AddScore((int)baseScore,comboTimerBonus);
            knocked = true;
        }
    }
}
