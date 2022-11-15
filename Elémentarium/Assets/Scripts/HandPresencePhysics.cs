using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float distanceShowRealHands;
    [SerializeField] private SkinnedMeshRenderer realHand;
    private Quaternion rotationDiference;
    private Vector3 rotationDiferenceInDegree;
    private void Start()
    {
        rb.maxAngularVelocity = 1000;
    }

    private void Update()
    {
        ShowRealHand();
    }

    private void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        rotationDiference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        rotationDiferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDiferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }

    void ShowRealHand()
    {
        if (Vector3.Distance(transform.position, target.position) > distanceShowRealHands)
        {
            realHand.enabled = true;
            return;
        }
        
        realHand.enabled = false;
    }
}
