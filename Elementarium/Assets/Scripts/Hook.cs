using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Pipe currentPipe;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Pipe>() != null && currentPipe==null)
        {
            currentPipe = other.GetComponentInParent<Pipe>();
            currentPipe.isOnHook = true;
            currentPipe.hookAttached = pivot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.GetComponentInParent<Pipe>() != null && currentPipe.Equals(other.GetComponentInParent<Pipe>()))
       {
           currentPipe.isOnHook = false;
           currentPipe.hookAttached = null;
           currentPipe = null;
       }
    }
}
