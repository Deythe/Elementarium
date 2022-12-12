using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    private Pipe currentPipe;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pipe>() != null)
        {
            currentPipe = other.GetComponent<Pipe>();
            currentPipe.isOnHook = true;
            currentPipe.hookAttached = pivot;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pipe>() != null)
        {
            currentPipe = other.GetComponent<Pipe>();
            currentPipe.isOnHook = false;
            currentPipe = null;
        }
    }
}
