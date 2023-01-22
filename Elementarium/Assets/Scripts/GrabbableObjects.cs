using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjects : MonoBehaviour
{
    public bool isGrabbed;

    public void Grab()
    {
        Debug.Log("grab");
        isGrabbed = true;
    }

    public void Release()
    {
        if (isGrabbed)
        {
            Debug.Log("release");
            GetComponent<Rigidbody>().isKinematic = false;
            isGrabbed = false;
        }
    }
}
