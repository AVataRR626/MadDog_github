using UnityEngine;
using System.Collections;

public class DogCatcher : MonoBehaviour
{
    public GameObject player;
    public GameObject endGameTree;
    public float catchRange = 1.75f;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        endGameTree = ScoreManager.instance.endGameTree;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dist2Dog = Vector3.Distance(transform.position, player.transform.position);

        if(dist2Dog <= catchRange)
        {
            player.GetComponent<PlayerMovement>().Speed = 0;

            endGameTree.SetActive(true);
        }
	}
}
