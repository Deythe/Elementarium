using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Brick : MonoBehaviour, IDestroyer
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float launchForce;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rb.isKinematic = false;
            rb.AddForce(Vector3.forward * launchForce);
        }
    }
}
