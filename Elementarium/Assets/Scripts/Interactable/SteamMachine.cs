using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamMachine : Interactible
{
    private Rigidbody rb;
    [SerializeField] private float upForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Collide(Transform e)
    {
        rb.AddForce(Vector3.up * upForce);
    }
}
