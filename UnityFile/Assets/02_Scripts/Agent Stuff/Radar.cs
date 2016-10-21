/*
NSI GameJam 11
Mad Dog

Radar class to detect poop and dogs...
-Matt Cabanag
*/

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Radar : MonoBehaviour
{
    public string [] tagList;//list of valid tags; order of tags implies priority
    public GameObject[] closestPerTag;
    public int contactSize = 0;
    public float ignoreRange = 10;
    public GameObject[] contactList = new GameObject[100];

    public GameObject ClosestContact
    {
        get
        {
            return contactList[closestContactIndex];
        }
    }

    Collider radarTrigger;
    int closestContactIndex = 0;
    float minDist = 1000;
    float [] minDistPerTag;

	// Use this for initialization
	void Start ()
    {
        radarTrigger = GetComponent<SphereCollider>();
        radarTrigger.isTrigger = true;
        minDistPerTag = new float[tagList.Length];
        closestPerTag = new GameObject[tagList.Length];
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckRange();
    }

    void OnTriggerEnter(Collider col)
    {
        //add object to my contact list....
        if(TagInList(col.tag,ref tagList))
        {
            contactList[contactSize] = col.gameObject;
            contactSize++;
            Physics.IgnoreCollision(col, radarTrigger, true);
        }
    }

    void CheckRange()
    {
        //initialise min distances...
        minDist = ignoreRange + 1;
        for(int i = 0; i < minDistPerTag.Length; i++)
        {
            minDistPerTag[i] = minDist;
        }

        //go through the list...
        for(int i = 0; i < contactSize; i++)
        {
            if(contactList[i] != null)
            {
                float range = Vector3.Distance(transform.position, contactList[i].transform.position);

                //keep track of the closest contact
                if(range<minDist)
                {
                    minDist = range;
                    closestContactIndex = i;
                }

                //keep track of the closest thing per tag
                int tagIndex = GetStringIndex(contactList[i].tag, ref tagList);

                if(range<minDistPerTag[tagIndex])
                {
                    if(range < ignoreRange)
                    { 
                        minDistPerTag[tagIndex] = range;
                        closestPerTag[tagIndex] = contactList[i];
                    }

                }
                else if (range > ignoreRange)
                {               
                    //delete from list if too far..
                    closestPerTag[tagIndex] = null;

                }

                //delete contacts that are too far away
                if (range >= ignoreRange)
                {
                    Debug.Log("TOO FAR NOW!");
                    Collider col = contactList[i].GetComponent<Collider>();
                    Physics.IgnoreCollision(col, radarTrigger, false);
                    RemoveContact(i);

                }
            }
        }
    }

    void RemoveContact(int i)
    {
        int cursor = i;

        while(cursor < contactSize && cursor < contactList.Length)
        {
            contactList[cursor] = contactList[cursor + 1];
            cursor++;
        }

        contactSize--;
    }

    //looks for the index of a given string target in a list,
    //-1 if target doesn't exist
    public static int GetStringIndex(string target, ref string [] list)
    {
        for(int i = 0; i < list.Length; i++)
        {
            if (list[i] == target)
                return i;
        }

        return -1;
    }

    public static bool TagInList(string tag, ref string [] list)
    {
        foreach (string t in list)
            if (t == tag)
                return true;

        return false;
    }
}
