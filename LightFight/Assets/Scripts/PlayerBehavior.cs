using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerBehavior : NetworkBehaviour {

	// How fast the player will move when they move.
	public float speed;
    public int maxHealth;
    public float resTime;

	// Gives us access to the RB2D, so we can move the player.
	private Rigidbody2D rb2d;
    public Gun gunType;
    public Tool flareType;
    public Tool toolType;
	public Transform firePoint;

    private Gun leGun;
    private Tool leTool;
    private Tool flareGun;
    private int health;
    private float resCooldown;
    private float timeAtRes;

    void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
        initializeArsenal();

        // Assign this to the Cooldown timers.
        GameObject cdTimer = GameObject.Find("Canvas/Utility Belt/Tool Box/CD Box");
        CooldownBox cdBox = cdTimer.GetComponent<CooldownBox>();
        cdBox.leTool = leTool; // 4/10 success gg. Doesn't work because they aren't networked.

        cdTimer = GameObject.Find("Canvas/Utility Belt/Flare Box/CD Box (2)");
        cdBox = cdTimer.GetComponent<CooldownBox>();
        cdBox.leTool = flareGun;

        health = maxHealth;
	}
    
    public override void OnStartLocalPlayer()
    {
        // The Local Player will be tinted blue
        GetComponent<SpriteRenderer>().color = Color.blue;

        //Assign this to the health bar.
        GameObject healthBar = GameObject.Find("Canvas/Health Bar Base/Health Bar Empty/Health Bar Full");
        HealthBar bar = healthBar.GetComponent<HealthBar>();
        bar.player = this; // 10/10 success gg.

 /*              // Assign this to the Cooldown timers.
               GameObject cdTimer = GameObject.Find("Canvas/Utility Belt/Tool Box/CD Box");
               CooldownBox cdBox = cdTimer.GetComponent<CooldownBox>();
               cdBox.leTool = leTool; // 4/10 success gg. Doesn't work because they aren't networked.
  */     

        //        cdTimer = GameObject.Find("Canvas/Uitility Belt/Flare Box/CD Box (2)");
        //        cdBox = cdTimer.GetComponent<CooldownBox>();
        //        cdBox.leTool = flareGun;

        // Finally, tell the camera to follow the player.
        GameObject camera = GameObject.Find("Main Camera");
        FollowCam fc = camera.GetComponent<FollowCam>();
        fc.following = gameObject;

    }

    void Update()
	{
        if (!isLocalPlayer)
            return;

        if (health <= 0)
        {
			scorePing();
			//Destroy (gameObject);
            Respawn();
        }

        if (Input.GetMouseButton(0))
        {
            leGun.AttemptFire();
        }

        if (Input.GetMouseButton(1))
        {
            leTool.AttemptFire();
        }

        if (Input.GetMouseButton(2))
        {
            flareGun.AttemptFire();
        }
	}

	/* Updates movement
	 * and rotation
	 */
	void FixedUpdate()
	{
        if (!isLocalPlayer)
            return;

            // Move
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.velocity = (movement * speed);
        
        // Rotate to Face the Mouse
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
	}

    /*
     * Pretty sure this is actually gonna be handled by
     * the game manager.
     */
    void Respawn()
    {
        if (gameObject.activeInHierarchy == true)
        {
//            gameObject.SetActive(false);
            transform.position = new Vector3(0, 0, 0); // Once we set up spawn positions that'll move.
            timeAtRes = Time.time + resTime;
        }
        resCooldown = timeAtRes - Time.time; // This is stuck in a loop. you should fix  that.
//        print("ResCooldown: " + resCooldown);
            health = maxHealth;
            gameObject.SetActive(true);
    }

    // Sets up all the weapons/tools on start.
    void initializeArsenal()
    {
        leGun = Instantiate(gunType, firePoint.position, transform.rotation) as Gun;
        leGun.transform.parent = transform;

        leTool = Instantiate(toolType, firePoint.position, transform.rotation) as Tool;
        leTool.transform.parent = transform;

        flareGun = Instantiate(flareType, firePoint.position, transform.rotation) as Tool;
        flareGun.transform.parent = transform;
    }

    void OnCollisionEnter2D(Collision2D otherObj)
    {
        if(otherObj.gameObject.tag == "Bolt")
        {
            health -= 10; //just hardcoding this bitch for now.
        }
    }

    public int getHealth()
    {
        return health;
    }

	public bool scorePing()
	{
		return true;
	}
}
