using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BlockedCube : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private XRGrabInteractable xrg;
    [SerializeField] private Transform woodParent;

    public void CheckState() 
    {
        if (woodParent.childCount < 2) 
        {
            rb.isKinematic = true;
            xrg.enabled = true;
        }
    }
}
