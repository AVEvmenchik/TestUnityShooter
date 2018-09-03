using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyBoundary : NetworkBehaviour
{


    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
