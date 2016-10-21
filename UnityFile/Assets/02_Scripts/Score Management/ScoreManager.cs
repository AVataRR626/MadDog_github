using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public float armingDelay = 0.5f;

    private float comboTimer;
    private int score;
    private int lastCombo;
    private int comboCounter;



    public bool Armed
    {
        get
        {
            return armingDelay <= 0;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
    }

    public int ComboCounter
    {
        get
        {
            return comboCounter;
        }
    }

    public int LastCombo
    {
        get
        {
            return lastCombo;
        }
    }

	// Use this for initialization
	void Start ()
    {
        //there can only be one!!
        if (instance != null)
            Destroy(gameObject);

        instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {

        ManageComboTimer();
        ManageArmingDelay();
    }

    void ManageComboTimer()
    {
        if(comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else if(comboCounter > 0)
        {
            lastCombo = comboCounter;
            comboCounter = 0;
        }
    }

    void ManageArmingDelay()
    {
        if (armingDelay > 0)
            armingDelay -= Time.deltaTime;
    }

    public void AddScore(int add)
    {
        //don't do anything until you're armed...
        if (!Armed)
            return;

        score += 1;

        if(comboTimer > 0)
        {
            comboCounter++;
            lastCombo = comboCounter;
        }
    }

    public void AddScore(int newScore, float newTime)
    {
        AddComboTimer(newTime);
        AddScore(newScore);
    }

    public void AddComboTimer(float newTime)
    {
        if (!Armed)
            return;

        comboTimer += newTime;
    }
}
