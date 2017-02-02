using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PrismBomb : MonoBehaviour {

    public float speed;
	public GameObject shotType;
    public Shotgun gun;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start()
    {
        gun = GetComponentInChildren<Shotgun>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D otherObj)
    {
        if (otherObj.gameObject.tag == "Bolt")
        {
			for (int i = 0; i < 60; i++) {
				var part = Instantiate (shotType, transform.position, Quaternion.AngleAxis (6 * i, transform.forward));
/*				SpriteRenderer sr = part.GetComponent<SpriteRenderer>();
				Color newColor = new Color (i, 0F, 0F, 1F);
				sr.color = newColor;
*/			}	
//            gun.CmdFire();
            Destroy(gameObject);
      }
    }
}
