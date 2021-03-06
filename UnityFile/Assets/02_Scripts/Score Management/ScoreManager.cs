﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Transform messageSpawnPoint;
    public Text messagePrefab;
    public GameObject endGameTree;

    public float armingDelay = 0.5f;
    public string specialMessage = "";
    public Text specialMessageDisplay;
    public int comboBonusThreshold = 5;
    public int comboBonusBasic = 5;
    public int comboBonusExtra = 10;

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

        //SyncSpecialMessage();
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
            comboTimer = 0;

            if (lastCombo > 1)
            {
                int baseBonus = 0;
                int xTraBonus = 0;

                if(lastCombo <= comboBonusThreshold)
                {
                    baseBonus = lastCombo * comboBonusBasic;
                }
                else
                {
                    baseBonus = lastCombo * comboBonusBasic;
                    xTraBonus = (lastCombo - comboBonusThreshold) * comboBonusExtra;
                }

                int totalComboScore = baseBonus + xTraBonus;

                AddScore(totalComboScore);

                string comboScoreMsg = totalComboScore + " BONUS POINTS";
                SpawnMessage(lastCombo + "x COMBO!!" + "\n" + comboScoreMsg);
            }
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

        score += add;

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

    [ContextMenu("TestMessageSync")]
    void SyncSpecialMessage()
    {

        specialMessageDisplay.gameObject.SetActive(true);
        specialMessageDisplay.SendMessage("Reset");

        if (specialMessageDisplay != null)
        {
            specialMessageDisplay.text = specialMessage;
            specialMessageDisplay.SendMessage("Pulse");
        }
    }

    public void SetSpecialMessage(string msg)
    {
        specialMessage = msg;

        SyncSpecialMessage();
    }

    public void SpawnMessage(string message)
    {
        Text newMessage = Instantiate(messagePrefab, messageSpawnPoint.transform.position, Quaternion.identity) as Text;
        newMessage.transform.parent = messageSpawnPoint;
        newMessage.text = message;
    }
}
