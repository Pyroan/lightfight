using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    public PlayerBehavior player;
    private Image bar;

	// Use this for initialization
	void Start () {
        bar = GetComponent<Image> ();
	}
	
	void Update ()
    {
        if (player != null)
            bar.fillAmount = (float)player.getHealth() / player.maxHealth;
	}
}
