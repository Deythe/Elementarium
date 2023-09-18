using System;
using UnityEngine;

public class Hammer : Interactible
{
    [SerializeField] private bool usingGravity;
    [SerializeField] private Rigidbody hammer_rb, pales_rb;
    [SerializeField] private float upAccelerationForce;
    [SerializeField] private Vector3 angularAcceleration, vectorForceEnclume;
    
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = hammer_rb.transform.localPosition;
    }

    protected override void Collide(Transform e)
    {
        pales_rb.angularVelocity += angularAcceleration;
        hammer_rb.AddRelativeForce(vectorForceEnclume * upAccelerationForce, ForceMode.Acceleration);
    }

    private void FixedUpdate()
    {
        if (!usingGravity)
        {
            if (vectorForceEnclume.x < 0)
            {
                if (Mathf.Abs(hammer_rb.transform.localPosition.x) - Mathf.Abs(initialPosition.x) > 0)
                {
                    hammer_rb.AddRelativeForce(vectorForceEnclume * upAccelerationForce, ForceMode.Acceleration);
                }
                else
                {
                    hammer_rb.velocity = Vector3.zero;
                }
            }
            else
            {
                if (Mathf.Abs(hammer_rb.transform.localPosition.x) - Mathf.Abs(initialPosition.x) < 0)
                {
                    hammer_rb.AddRelativeForce(-vectorForceEnclume * upAccelerationForce, ForceMode.Acceleration);
                }
                else
                {
                    hammer_rb.velocity = Vector3.zero;
                }
            }
        }
    }
}
