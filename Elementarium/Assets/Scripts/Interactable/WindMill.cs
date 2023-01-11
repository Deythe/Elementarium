using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill: Interactible, ICompleted
{
    [SerializeField] private GameObject pales;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 angularAcceleration;
     
    protected override void Collide(Transform e)
    {
        rb.angularVelocity += angularAcceleration;
        interactionEvent.Invoke();
    }
    
    public bool getCompletedCondition()
    {
        return rb.angularVelocity.magnitude > 0.3;
    }
}
