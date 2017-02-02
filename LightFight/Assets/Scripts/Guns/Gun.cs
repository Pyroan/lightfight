using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class Gun : NetworkBehaviour {

    public float fireRate; // Time in seconds between shots
    private float nextFire;
    public GameObject shotType;

    public void AttemptFire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CmdFire();
        }
    }

    //[Command]
    public abstract void CmdFire();
}
