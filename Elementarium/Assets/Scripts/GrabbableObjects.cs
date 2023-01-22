using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjects : MonoBehaviour
{
    public bool isGrabbed;
    public int grabbers;

    public void Grab()
    {
        grabbers ++;
        isGrabbed = grabbers > 0;
    }

    public void Release()
    {
        if (isGrabbed)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            grabbers --;
            isGrabbed = grabbers > 0;
        }
    }
}
