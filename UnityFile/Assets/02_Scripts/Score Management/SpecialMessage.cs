using UnityEngine;
using System.Collections;

public class SpecialMessage : MonoBehaviour
{
    public string specialMessage = "This is SPARTA!";

    public void Trigger()
    {
        ScoreManager.instance.SetSpecialMessage(specialMessage);
    }

}
