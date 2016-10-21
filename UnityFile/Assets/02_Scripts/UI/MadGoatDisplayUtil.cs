using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class MadGoatDisplayUtil : MonoBehaviour
{
    public enum DisplayMode {score, lastCombo};
    public DisplayMode displayMode;

    private Text txt;

	// Use this for initialization
	void Start ()
    {
        txt = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(displayMode == DisplayMode.score)
            txt.text = ScoreManager.instance.Score.ToString();

        if(displayMode == DisplayMode.lastCombo)
            txt.text = ScoreManager.instance.LastCombo.ToString();

    }
}
