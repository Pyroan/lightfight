using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;

public class MirrorGun : Tool {

    
    public override void Fire()
    {
        var x = (GameObject)(Instantiate(tool, transform.position, transform.rotation));
        NetworkServer.Spawn(x);
    }
}
