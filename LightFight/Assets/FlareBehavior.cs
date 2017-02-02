using UnityEngine;
using System.Collections;

// Honestly, flares don't serve much purpose ATM, since
// Fog of War isn't a thing yet.
public class FlareBehavior : MonoBehaviour {

    public float speed;
    public float lifeTime; // How long a flare will last in seconds.

    private float deathTime;
    private Rigidbody2D rb2d;


	void Start () {
        Physics2D.IgnoreLayerCollision(10, 9);
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
	}

    // Flares bounce off mirrors just because.
    // (I guess because they're round) they don't need to be "live" to reflect.
    void OnCollisionEnter2D(Collision2D otherObj)
    {
        if (otherObj.gameObject.tag == "Mirror")
        {
            float m = otherObj.transform.rotation.eulerAngles.z; // Angle of the mirror
            float a = transform.rotation.eulerAngles.z;          // Angle of the bolt
            float theta = ((2 * m) - a) + 180; // Math.html (see ShotBehavior.OnCollisionEnter2D())

            // Rotates the flare
            transform.rotation = Quaternion.AngleAxis(theta, transform.forward);
            rb2d.velocity = transform.up * rb2d.velocity.magnitude;
        }
    }
}
