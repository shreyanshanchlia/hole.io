using System;
using UnityEngine;

public class CollectableGravity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Collectable>())
        {
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Collectable>())
        {
            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
