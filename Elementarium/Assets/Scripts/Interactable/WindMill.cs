using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill: Interactible, ICompleted
{
    Rigidbody rb;
    [SerializeField] Vector3 eulerAngleVelocity;
    [SerializeField] private float maxRotationSpeed;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxRotationSpeed;
    }

    protected override void Collide(Transform collid)
    {
        rb.AddRelativeTorque(eulerAngleVelocity * Time.deltaTime, ForceMode.Force);
        interactionEvent.Invoke();
    }

    public bool getCompletedCondition()
    {
        return rb.angularVelocity.magnitude >= 4.5f;
    }

    public bool getResetCondition()
    {
        return false;
    }
}