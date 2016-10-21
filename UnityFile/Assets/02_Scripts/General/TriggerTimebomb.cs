/*
NSI GameJam 11
Mad Dog

Gets tripped when anything enters its trigger range;
-Matt Cabanag
*/

using UnityEngine;
using System.Collections;

public class TriggerTimebomb : MonoBehaviour
{

    public float timer = 3;
    public string[] tagList;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (Radar.TagInList(col.tag, ref tagList))
            Destroy(transform.root.gameObject, timer);
    }
}
