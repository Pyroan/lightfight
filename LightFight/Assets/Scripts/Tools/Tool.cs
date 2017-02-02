using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class Tool : NetworkBehaviour {
    // This works very similarly to Gun. I feel disgusting.
    public float cooldown;
    public GameObject tool;
    private float remainingCooldown;
    private float timeWhenAvailable;
	
	// Update is called once per frame
    void Update()
    {
        remainingCooldown = timeWhenAvailable - Time.time;
    }

	public void AttemptFire ()
    {
	    if (Time.time > timeWhenAvailable)
        {
            timeWhenAvailable = Time.time + cooldown;
            Fire();
        }
	}

    public abstract void Fire();

    public float getCooldown()
    {
        return cooldown;
    }

    public float getRemainingCooldown()
    {
        return remainingCooldown;
    }
}
