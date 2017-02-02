using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Pistol : Gun {

    [Command]
    override public void CmdFire()
    {
        // Makes a bullet (Fucking Gasp)
        GameObject shot = (GameObject)Instantiate(shotType, transform.position, transform.rotation);
        NetworkServer.Spawn(shot);
//        Instantiate(shotType, transform.position, transform.rotation);
    }
}
