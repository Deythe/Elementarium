using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill: Interactible
{
    [SerializeField] private GameObject pales;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 angularAcceleration;
     
    protected override void Collide(Element e)
    {
        //pales.transform.Rotate(Vector3.forward, 10);
        rb.angularVelocity += angularAcceleration;
    }
}
