using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill: Interactible
{
    [SerializeField] private GameObject pales;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 angularAcceleration;
     
    protected override void Collide(Transform e)
    {
        rb.angularVelocity += angularAcceleration;
    }
}