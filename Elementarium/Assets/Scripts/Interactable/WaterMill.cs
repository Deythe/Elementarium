using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMill : Interactible
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 angularAcceleration;
     
    protected override void Collide(Transform e)
    {
        rb.angularVelocity += angularAcceleration;
    }
}
