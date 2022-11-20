using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamMachine : MonoBehaviour, ISteamReceiver
{
    private Rigidbody rb;
    [SerializeField] private float upForce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Power();
    }

    public void Power()
    {
        rb.AddForce(Vector3.up * upForce);
    }
}
