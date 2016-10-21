using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour
{
    private Text txt;

	// Use this for initialization
	void Start ()
    {
        txt = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        txt.text = ScoreManager.instance.Score.ToString();
	}
}
