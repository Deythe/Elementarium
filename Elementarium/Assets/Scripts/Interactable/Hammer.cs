using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Interactible
{
    [SerializeField] private Rigidbody hammer_rb, pales_rb;
    [SerializeField] private float upAccelerationForce;
    [SerializeField] private Vector3 angularAcceleration;
     
    protected override void Collide(Transform e)
    {
        if (e.GetComponentInParent<Element>().GetID() == ElementData.ID.AIR)
        {
            pales_rb.angularVelocity += angularAcceleration;
            hammer_rb.AddForce(Vector3.up * upAccelerationForce, ForceMode.Acceleration);
        }
    }
}
