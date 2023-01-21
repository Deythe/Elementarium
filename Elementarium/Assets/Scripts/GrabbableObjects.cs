using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjects : MonoBehaviour
{
    public bool isGrabbed;

    public void Grab()
    {
        isGrabbed = true;
    }

    public void Release()
    {
        isGrabbed = false;
    }
}
