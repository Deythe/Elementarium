using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LerpButtons : MonoBehaviour
{
    [SerializeField] private UnityEvent onPressed, onReleased;

    [SerializeField] private float maxWeight;

    private Rigidbody rb;
    public GameObject collider;

    private void OnCollisionEnter(Collision collision)
    {
        rb = collision.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
        collider = collision.gameObject;
    }

    private bool IsTooHeavy()
    {
        return (rb.mass >= maxWeight);
    }
    
    
}