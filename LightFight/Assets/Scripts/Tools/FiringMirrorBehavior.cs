using UnityEngine;
using System.Collections;

public class FiringMirrorBehavior : MonoBehaviour {

    public float speed;
    public float lifeTime; // How long a thrown mirror will last in seconds.

    private float deathTime;
    private Rigidbody2D rb2d;


	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
        deathTime = Time.time + lifeTime;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > deathTime)
        {
            Destroy(gameObject);
        }
        if (rb2d.velocity.magnitude == 0)
        {
            rb2d.isKinematic = true;
        }
	}
}
