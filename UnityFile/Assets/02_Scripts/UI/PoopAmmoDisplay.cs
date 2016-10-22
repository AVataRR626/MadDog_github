using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class PoopAmmoDisplay : MonoBehaviour
{
    public int maxValue;

    public float value;

    private Image img;
    private PlayerMovement player;

	// Use this for initialization
	void Start ()
    {
        img = GetComponent<Image>();
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        value = (float)player.PoopAmmo / (float)maxValue;
        value = Mathf.Clamp(value, 0, maxValue);
        img.fillAmount = value;
	}
}
