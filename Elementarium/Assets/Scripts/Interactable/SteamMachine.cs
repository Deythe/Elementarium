using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamMachine : Interactible
{
    private Rigidbody rb;
    [SerializeField] private float upForce;
    private bool frozen;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void Collide(Transform e)
    {
        if (e.GetID() == ElementData.ID.STEAM)
        {
            rb.AddForce(Vector3.up * upForce);
        } else
        {
            if (frozen) return;
            StartCoroutine(Freeze());
        }
    }

    private IEnumerator Freeze()
    {
        Debug.Log("I'm frozen");
        frozen = true;
        yield return new WaitForSeconds(2);
        frozen = false;
        Debug.Log("I'm not frozen anymore");
    }
}
