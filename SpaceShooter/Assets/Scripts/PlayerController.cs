using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : NetworkBehaviour {


    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        var rigbody = GetComponent<Rigidbody>();

        rigbody.velocity = movement * speed;

        rigbody.position = new Vector3
        (
                Mathf.Clamp (rigbody.position.x,  boundary.xMin, boundary.xMax ),
                0.0f,
                Mathf.Clamp (rigbody.position.z, boundary.zMin, boundary.zMax)

            );
        rigbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigbody.velocity.x * -tilt);
    }



    [Command]
    void CmdShoot()
    {

       Shoot();
        RpcShoot();
    }

    [ClientRpc]
    void RpcShoot()
    {
        if (!isServer)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bolt = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
        GetComponent<AudioSource>().Play();
        //NetworkServer.Spawn(bolt);
    }


    void Update () 
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            CmdShoot();
           
        }
	} 
}
