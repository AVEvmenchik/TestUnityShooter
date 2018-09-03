using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyByTime : NetworkBehaviour {

    public float lifeTime;

	void Start () 
    {
        Destroy(gameObject,lifeTime);
	}
	
	
}
