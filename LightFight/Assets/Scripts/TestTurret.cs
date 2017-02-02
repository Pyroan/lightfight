using UnityEngine;
using System.Collections;

public class TestTurret : MonoBehaviour {
    // How fast the player will move when they move.
    public float speed;
	public bool isUsingAi;

    // Gives us access to the RB2D, so we can move the player.
    private Rigidbody2D rb2d;

    public GameObject shot;
    public Transform firePoint;
    public float fireRate;
    private float nextFire;

	public int maxHealth;
	private int health;

	public GameObject target;
	private float colTime = 1;
	private float timeTilLive;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
		timeTilLive = Time.time + colTime;
		health = maxHealth;
    }

    void Update()
    {
		if (health == 0)
		{
			scorePing ();
			Destroy (gameObject);
		}
		target = GameObject.Find ("PlayerTest 1(Clone)");

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, firePoint.position, firePoint.rotation);
        }
		if (!isUsingAi)
			return;
		
			// Rotate to face player.
			Vector3 playerLocation = target.transform.position;
			Vector3 pos = transform.position - playerLocation;
			float rot = Mathf.Atan2 (pos.y, pos.x) * Mathf.Rad2Deg + 90;
			transform.rotation = Quaternion.AngleAxis (rot, Vector3.forward);
			rb2d.velocity = playerLocation * speed;
	}

	void OnCollisionEnter2D(Collision2D otherObj)
	{
		if (Time.time < timeTilLive)
			return;
		
/*		if (otherObj.gameObject.tag == "Border") {
			rb2d.velocity = -1 * rb2d.velocity;
		}
*/		if(otherObj.gameObject.tag == "Bolt")
		{
			health -= 10; //just hardcoding this bitch for now.
		}
		timeTilLive = Time.time + colTime;
	}
	public bool scorePing()
	{
		return true;
	}
}
