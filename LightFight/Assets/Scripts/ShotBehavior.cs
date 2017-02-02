using UnityEngine;
using System.Collections;

/*
 * Handles the movement/collision handling with all bullets.
 * Reminder that all of my classes are basically a mess.
 * -- Evan
 */
public class ShotBehavior : MonoBehaviour {

	public float speed;     // The speed that the bullet will move
    public float minLiving; // How long it must exist until it can reflect.
    public int damage; // how much damage a shot will do and damn i need to extend this class.

	/*
	 * Upon collision, there was an issue whereby the bullets would constantly be reflecting on mirrors
	 * if they touched one which got them stuck.
	 * 
	 * My "temporary" workaround for this was to turn off bullet collision w/ mirrors for a 
	 * short amount of time upon being reflected.
	 */
    private bool isLive;    // In this case, "live" means able to collide (and/or reflect).
    private float timeLive; // In-game time when the bullet will be live.
	private Rigidbody2D rb2d; // The bullet's "body", used for the physics handling/movement
	
	/*
	 * Start() is called when the object is created (like a constructor)
	 */
	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();
		rb2d.velocity = transform.up * speed;
        timeLive = Time.time; // + minLiving;
        Physics2D.IgnoreLayerCollision(8, 8); // This means bullets can't touch bullets.
    }

	/*
	 * Update() is called once per frame
	 */
    void Update()
    {
        if (Time.time <= timeLive)
            isLive = false; // Remember, "live" basically means allowed to reflect.
        else
            isLive = true;

		// This is ideally temporary. Bullets disappear if they go below a certain velocity.
		// So far this only affects shotgun shells since pistol ammo doesn't slow down.
        if (rb2d.velocity.magnitude < .5) Destroy(gameObject);
    }

	/*
	 * How a shot will handle interaction with each material.
	 */
	void OnCollisionEnter2D(Collision2D otherObj) {
        // Deletes itself on collision with Wall.
        if (otherObj.gameObject.tag == "Border")
			Destroy (gameObject);

        // Reflects on collision with Mirrors
		if (otherObj.gameObject.tag == "Mirror")
        {
            if (isLive)
            {
                float m = otherObj.transform.rotation.eulerAngles.z; // Angle of the mirror
                float a = transform.rotation.eulerAngles.z;          // Angle of the bolt
                float theta = ((2 * m) - a) + 180;  // Thank you TJ For helping me work out math.
                
                // Rotates the bolt, and resets the life timer.
                transform.rotation = Quaternion.AngleAxis(theta, transform.forward);
                rb2d.velocity = transform.up * rb2d.velocity.magnitude;
                timeLive = Time.time + minLiving;
            } else
            {
                Destroy(gameObject);     
            }
        }
		// Deletes self on hitting a player
        if (otherObj.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
	}


}
