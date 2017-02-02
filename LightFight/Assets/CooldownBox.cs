using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CooldownBox : MonoBehaviour {

    public Tool leTool;
    private Image box;

	// Use this for initialization
	void Start () {
        box = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (leTool != null)
            box.fillAmount = leTool.getRemainingCooldown() / leTool.getCooldown();
    }
}
