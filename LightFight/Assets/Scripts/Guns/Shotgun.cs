using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class Shotgun : Gun {
    public float spread; // The total angular spread of the shots.
    public int n; // Number of shots
    private float inSpread; // Individual spread, the angle between each shot.
    void Start()
    {
        inSpread = spread / (n - 1);        
    }

    [Command]
    override public void CmdFire()
    {
        float theta = transform.rotation.eulerAngles.z - (spread/2);
        for (int i = 0; i < n; i ++)
        {
            var shot = (GameObject)Instantiate(shotType, transform.position, 
                Quaternion.AngleAxis(theta, transform.forward));
            NetworkServer.Spawn(shot);
            theta += inSpread;
        }
    }
}
