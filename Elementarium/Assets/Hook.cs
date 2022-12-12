using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pipe>() != null)
        {
            other.GetComponent<Pipe>().isOnHook = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Pipe>() != null)
        {
            other.GetComponent<Pipe>().isOnHook = false;
        }
    }
}
