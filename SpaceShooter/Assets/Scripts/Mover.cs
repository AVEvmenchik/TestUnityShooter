using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mover : NetworkBehaviour {

    public float speed;
	
	void Start () {

        var rigbody = GetComponent<Rigidbody>();

        rigbody.velocity = transform.forward * speed;
		
	}
	
}
